using Api.eCommerce.Database;
using Library.eCommerce.Models;

namespace Api.eCommerce.EC
{
    public class InventoryEC
    {
        public List<Item?> Get()
        {
            return InventoryDatabase.Inventory;
        }

        public Item? AddOrUpdate(Item item)
        {
            if (item == null)
            {
                return null;
            }

            if (item.Id == 0)
            {
                item.Id = InventoryDatabase.LastKey_Item + 1;
                InventoryDatabase.Inventory.Add(item);
            }
            else
            {
                var existing = InventoryDatabase.Inventory.FirstOrDefault(p => p?.Id == item.Id);
                if (existing != null)
                {
                    InventoryDatabase.Inventory.Remove(existing);
                }
                InventoryDatabase.Inventory.Add(item);
            }

            InventoryDatabase.SaveChanges();
            return item;
        }

        public Item? Delete(int id)
        {
            var toDelete = InventoryDatabase.Inventory.FirstOrDefault(p => p?.Id == id);
            if (toDelete != null)
            {
                InventoryDatabase.Inventory.Remove(toDelete);
                InventoryDatabase.SaveChanges();
            }
            return toDelete;
        }
    }
}
