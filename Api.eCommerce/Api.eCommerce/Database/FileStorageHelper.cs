using System.Xml;
using Library.eCommerce.Models;
using Newtonsoft.Json;


namespace Api.eCommerce.Database
{
    public static class FileStorageHelper
    {
        private static readonly string filePath = "inventory.json";

        public static List<Item?> LoadInventory()
        {
            if (!File.Exists(filePath))
            {
                return new List<Item?>();
            }

            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<Item?>>(json) ?? new List<Item?>();
        }

        public static void SaveInventory(List<Item?> inventory)
        {
            var json = JsonConvert.SerializeObject(inventory, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
    }
}
