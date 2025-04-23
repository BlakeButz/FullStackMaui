using Library.eCommerce.DTO;
using Library.eCommerce.Models;
using Library.eCommerce.Services;
using Library.eCommerce.Utilities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Library.eCommerce.Services
{
    public class ShoppingCartService
    {
        private ProductServiceProxy _prodSvc = ProductServiceProxy.Current;
        private List<Item> items;

        public List<Item> CartItems => items;

        private static ShoppingCartService? instance;

        public static ShoppingCartService Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new ShoppingCartService();
                }

                return instance;
            }
        }

        private ShoppingCartService()
        {
            var response = new WebRequestHandler().Get("/Cart").Result;
            items = JsonConvert.DeserializeObject<List<Item>>(response) ?? new List<Item>();
        }

        public Item? AddOrUpdate(Item item)
        {
            var existingInvItem = _prodSvc.GetById(item.Id);
            if (existingInvItem == null || existingInvItem.Quantity == 0)
            {
                return null;
            }

            existingInvItem.Quantity--;
            _prodSvc.AddOrUpdate(existingInvItem);

            var localItem = items.FirstOrDefault(i => i.Id == item.Id);
            if (localItem == null)
            {
                localItem = new Item(item) { Quantity = 1 };
                items.Add(localItem);
            }
            else
            {
                localItem.Quantity++;
            }

            PostDeltaToBackend(localItem.Product, 1);
            return existingInvItem;
        }

        public Item? ReturnItem(Item? item)
        {
            if (item?.Id <= 0 || item == null) return null;

            var localItem = items.FirstOrDefault(c => c.Id == item.Id);
            if (localItem != null)
            {
                localItem.Quantity--;

                var inventoryItem = _prodSvc.Products.FirstOrDefault(p => p.Id == localItem.Id);
                if (inventoryItem == null)
                {
                    _prodSvc.AddOrUpdate(new Item(localItem));
                }
                else
                {
                    inventoryItem.Quantity++;
                    _prodSvc.AddOrUpdate(inventoryItem);
                }

                if (localItem.Quantity <= 0)
                {
                    items.Remove(localItem);
                }

                PostDeltaToBackend(item.Product, -1);
            }

            return localItem;
        }

        public void FinalizeCheckout()
        {
            items.Clear();
            new WebRequestHandler().Post("/Cart/clear", new { }).Wait();
        }

        public string GenerateReceipt()
        {
            return ReceiptGenerator.GenerateReceipt(items);
        }

        private void PostDeltaToBackend(ProductDTO product, int delta)
        {
            var payload = new Item
            {
                Id = product.Id,
                Product = new ProductDTO(product) { Id = product.Id },
                Quantity = delta
            };

            new WebRequestHandler().Post("/Cart", payload).Wait();
        }
    }
}
