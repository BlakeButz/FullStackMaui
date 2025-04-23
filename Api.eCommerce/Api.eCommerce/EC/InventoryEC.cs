using Api.eCommerce.Database;
using Library.eCommerce.Models;

namespace Api.eCommerce.EC
{
    public class InventoryEC
    {
        public List<Item?> Get()
        {
            return FakeDatabase.Inventory;
        }

        public Item? AddOrUpdate(Item item)
        {
            if (item == null)
            {
                return null;
            }

            if (item.Id == 0)
            {
                item.Id = FakeDatabase.LastKey_Item + 1;
                FakeDatabase.Inventory.Add(item);
            }
            else
            {
                var existing = FakeDatabase.Inventory.FirstOrDefault(p => p?.Id == item.Id);
                if (existing != null)
                {
                    FakeDatabase.Inventory.Remove(existing);
                }
                FakeDatabase.Inventory.Add(item);
            }

            FakeDatabase.SaveChanges();
            return item;
        }

        public Item? Delete(int id)
        {
            var toDelete = FakeDatabase.Inventory.FirstOrDefault(p => p?.Id == id);
            if (toDelete != null)
            {
                FakeDatabase.Inventory.Remove(toDelete);
                FakeDatabase.SaveChanges();
            }
            return toDelete;
        }
    }
}
