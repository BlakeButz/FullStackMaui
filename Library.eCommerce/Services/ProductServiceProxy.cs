using Library.eCommerce.Models;
using Library.eCommerce.Utilities;
using Newtonsoft.Json;

namespace Library.eCommerce.Services
{
    public class ProductServiceProxy
    {
        private ProductServiceProxy()
        {
            var productPayload = new WebRequestHandler().Get("/Inventory").Result;
            Products = JsonConvert.DeserializeObject<List<Item>>(productPayload) ?? new List<Item>();
        }

        private static ProductServiceProxy? instance;
        private static readonly object instanceLock = new();

        public static ProductServiceProxy Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductServiceProxy();
                    }
                }

                return instance;
            }
        }

        public List<Item> Products { get; private set; }

        public Item AddOrUpdate(Item item)
        {
            var response = new WebRequestHandler().Post("/Inventory", item).Result;
            var newItem = JsonConvert.DeserializeObject<Item>(response);

            if (newItem == null)
            {
                return item;
            }

            // Ensure the Product ID matches the Item ID
            if (newItem.Product != null)
            {
                newItem.Product.Id = newItem.Id;
            }

            if (item.Id == 0)
            {
                Products.Add(newItem);
            }
            else
            {
                var index = Products.FindIndex(p => p.Id == newItem.Id);
                if (index >= 0)
                {
                    Products[index] = newItem;
                }
            }

            return newItem;
        }

        public Item? Delete(int id)
        {
            if (id == 0)
            {
                return null;
            }

            var result = new WebRequestHandler().Delete($"/Inventory/{id}").Result;
            var deletedItem = JsonConvert.DeserializeObject<Item>(result);

            if (deletedItem != null)
            {
                var existing = Products.FirstOrDefault(p => p.Id == id);
                if (existing != null)
                {
                    Products.Remove(existing);
                }
            }

            return deletedItem;
        }

        public Item? GetById(int id)
        {
            return Products.FirstOrDefault(p => p.Id == id);
        }
    }
}
