using Api.eCommerce.Database;
using Library.eCommerce.Models;

namespace Api.eCommerce.EC
{
    public class CartEC
    {
        public List<Item> Get() => CartDatabase.CartItems;

        public Item? AddOrUpdate(Item item)
        {
            var existing = CartDatabase.CartItems.FirstOrDefault(i => i.Id == item.Id);
            if (existing != null)
            {
                existing.Quantity += item.Quantity;

                if (existing.Quantity <= 0)
                {
                    CartDatabase.CartItems.Remove(existing);
                }
            }
            else if (item.Quantity > 0)
            {
                // Ensure product info is pulled from inventory
                var inventoryItem = FakeDatabase.Inventory.FirstOrDefault(i => i.Id == item.Id);
                if (inventoryItem != null)
                {
                    item.Product = inventoryItem.Product;
                }
                CartDatabase.CartItems.Add(item);
            }

            CartDatabase.Save();
            return item;
        }

        public Item? Delete(int id)
        {
            var existing = CartDatabase.CartItems.FirstOrDefault(i => i.Id == id);
            if (existing != null)
            {
                CartDatabase.CartItems.Remove(existing);
                CartDatabase.Save();
            }

            return existing;
        }

        public void Clear()
        {
            CartDatabase.Clear();
        }
    }
}