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
        private static object instanceLock = new object();

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

        private int LastKey => Products.Any() ? Products.Select(p => p.Id).Max() : 0;

        public Item AddOrUpdate(Item item)
        {
            if (item.Id == 0)
            {
                item.Id = LastKey + 1;
                Products.Add(item);
            }

            return item;
        }

        public Item? Delete(int id)
        {
            if (id == 0)
            {
                return null;
            }

            var item = Products.FirstOrDefault(p => p.Id == id);
            if (item != null)
            {
                Products.Remove(item);
            }

            return item;
        }

        public Item? GetById(int id)
        {
            return Products.FirstOrDefault(p => p.Id == id);
        }
    }
}
