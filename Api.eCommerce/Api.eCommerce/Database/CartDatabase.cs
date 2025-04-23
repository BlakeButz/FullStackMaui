using Library.eCommerce.Models;
using Newtonsoft.Json;

namespace Api.eCommerce.Database
{
    public static class CartDatabase
    {
        private static readonly string filePath = "cart.json";
        private static List<Item> cartItems;

        static CartDatabase()
        {
            cartItems = File.Exists(filePath)
                ? JsonConvert.DeserializeObject<List<Item>>(File.ReadAllText(filePath)) ?? new List<Item>()
                : new List<Item>();
        }

        public static List<Item> CartItems => cartItems;

        public static void Save()
        {
            var json = JsonConvert.SerializeObject(cartItems, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public static void Clear()
        {
            cartItems.Clear();
            Save();
        }
    }
}
