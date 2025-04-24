using Library.eCommerce.DTO;
using Library.eCommerce.Models;

namespace Api.eCommerce.Database
{
    public static class InventoryDatabase
    {
        private static List<Item?> inventory;

        static InventoryDatabase()
        {
            inventory = FileStorageHelper.LoadInventory();

            // Seed if empty
            if (!inventory.Any())
            {
                inventory = new List<Item?>
                {
                    new Item { Product = new ProductDTO { Id = 1, Name = "Product 1 WEB", Price = 10.99M }, Id = 1, Quantity = 1 },
                    new Item { Product = new ProductDTO { Id = 2, Name = "Product 2 WEB", Price = 14.50M }, Id = 2, Quantity = 2 },
                    new Item { Product = new ProductDTO { Id = 3, Name = "Product 3 WEB", Price = 5.75M }, Id = 3, Quantity = 3 }
                };
                FileStorageHelper.SaveInventory(inventory);
            }
        }

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

        public static List<Item?> Inventory => inventory;

        public static void SaveChanges()
        {
            FileStorageHelper.SaveInventory(inventory);
        }
    }
}
