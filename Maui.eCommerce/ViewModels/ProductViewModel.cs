using Library.eCommerce.Models;
using Library.eCommerce.Services;

namespace Maui.eCommerce.ViewModels
{
    public class ProductViewModel
    {
        public string? Name
        {
            get => Model?.Product?.Name ?? string.Empty;
            set
            {
                if (Model?.Product != null && Model.Product.Name != value)
                {
                    Model.Product.Name = value;
                }
            }
        }

        public int? Quantity
        {
            get => Model?.Quantity;
            set
            {
                if (Model != null && Model.Quantity != value)
                {
                    Model.Quantity = value;
                }
            }
        }

        public decimal Price
        {
            get => Model?.Product?.Price ?? 0;
            set
            {
                if (Model?.Product != null && Model.Product.Price != value)
                {
                    Model.Product.Price = value;
                }
            }
        }

        public Item? Model { get; set; }

        public void AddOrUpdate()
        {
            ProductServiceProxy.Current.AddOrUpdate(Model);
        }

        public void Undo()
        {
            if (cachedModel != null)
            {
                ProductServiceProxy.Current.AddOrUpdate(new Item(cachedModel));
            }
        }

        private Item? cachedModel;

        public ProductViewModel()
        {
            Model = new Item
            {
                Product = new Library.eCommerce.DTO.ProductDTO(),
                Quantity = 1
            };
            cachedModel = null;
        }

        public ProductViewModel(Item? model)
        {
            Model = model;
            if (model != null)
            {
                cachedModel = new Item(model);
            }
        }
    }
}
