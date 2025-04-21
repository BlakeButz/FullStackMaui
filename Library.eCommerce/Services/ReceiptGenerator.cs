using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Library.eCommerce.Models;
using System.Text;

namespace Library.eCommerce.Services
{
    public static class ReceiptGenerator
    {
        public static string GenerateReceipt(List<Item> cartItems)
        {
            StringBuilder receipt = new();
            decimal subtotal = 0;

            receipt.AppendLine("🧾 Itemized Receipt");
            receipt.AppendLine("----------------------------");

            foreach (var item in cartItems)
            {
                var price = item.Product?.Price ?? 0;
                var quantity = item.Quantity ?? 0;
                var total = price * quantity;

                receipt.AppendLine($"{item.Product?.Name} x{quantity} @ {price:C} = {total:C}");
                subtotal += total;
            }

            decimal tax = subtotal * 0.07m;
            decimal final = subtotal + tax;

            receipt.AppendLine("----------------------------");
            receipt.AppendLine($"Subtotal: {subtotal:C}");
            receipt.AppendLine($"Tax (7%): {tax:C}");
            receipt.AppendLine($"Total: {final:C}");

            return receipt.ToString();
        }
    }
}
