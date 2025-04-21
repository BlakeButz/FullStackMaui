using Library.eCommerce.DTO;
using Library.eCommerce.Models;
using Spring2025_Samples.Models;

namespace Api.eCommerce.Database
{
    public static class FakeDatabase
    {
        private static List<Item?> inventory = new List<Item?>
        {
            new Item{ Product = new ProductDTO{Id = 1, Name ="Product 1 WEB", Price = 10.99M }, Id = 1, Quantity = 1 },
            new Item{ Product = new ProductDTO{Id = 2, Name ="Product 2 WEB", Price = 14.50M }, Id = 2 , Quantity = 2 },
            new Item{ Product = new ProductDTO{Id = 3, Name ="Product 3 WEB", Price = 5.75M }, Id=3 , Quantity = 3 }
        };


        public static int LastKey_Item
        {
            get
            {
                if (!inventory.Any())
                {
                    return 0;
                }

                return inventory.Select(p => p?.Id ?? 0).Max();
            }
        }
        public static List<Item?> Inventory
        {
            get
            {
                return inventory;
            }
        }


    }
}
