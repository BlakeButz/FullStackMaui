using Library.eCommerce.Models;
using Microsoft.VisualBasic;
using Newtonsoft.Json;


namespace Library.eCommerce.Utilities
{
    public static class CartStorageHelper
    {
        private static readonly string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "cart.json");

        public static List<Item> LoadCart()
        {
            if (!File.Exists(filePath))
            {
                return new List<Item>();
            }

            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<Item>>(json) ?? new List<Item>();
        }

        public static void SaveCart(List<Item> cartItems)
        {
            var json = JsonConvert.SerializeObject(cartItems, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
    }
}
