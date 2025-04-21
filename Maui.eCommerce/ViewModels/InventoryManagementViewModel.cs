using Library.eCommerce.DTO;
using Library.eCommerce.Models;
using Library.eCommerce.Services;
using Spring2025_Samples.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Maui.eCommerce.ViewModels
{
    public class InventoryManagementViewModel : INotifyPropertyChanged
    {
        public Item? SelectedProduct { get; set; }
        public string? Query { get; set; }
        private ProductServiceProxy _svc = ProductServiceProxy.Current;

        public event PropertyChangedEventHandler? PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (propertyName is null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RefreshProductList()
        {
            NotifyPropertyChanged(nameof(Products));
        }

        public ObservableCollection<Item?> Products
        {
            get
            {
                var filteredList = _svc.Products
                    .Where(p => p?.Product?.Name?.ToLower().Contains(Query?.ToLower() ?? string.Empty) ?? false);

                return new ObservableCollection<Item?>(filteredList);
            }
        }

        public Item? Delete()
        {
            var item = _svc.Delete(SelectedProduct?.Id ?? 0);
            NotifyPropertyChanged(nameof(Products));
            return item;
        }

        public void AddProduct(string name, decimal price)
        {
            var newProduct = new ProductDTO
            {
                Id = ProductServiceProxy.Current.Products.Max(p => p.Id) + 1,
                Name = name,
                Price = price
            };

            var newItem = new Item
            {
                Product = newProduct,
                Quantity = 1
            };

            _svc.AddOrUpdate(newItem);
            RefreshProductList();
        }

        public void UpdateProduct(string name, decimal price)
        {
            if (SelectedProduct == null) return;

            SelectedProduct.Product.Name = name;
            SelectedProduct.Product.Price = price;

            _svc.AddOrUpdate(SelectedProduct);
            RefreshProductList();
        }
    }
}
