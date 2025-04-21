using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Library.eCommerce.Services;

namespace Maui.eCommerce.ViewModels
{
    public class CheckoutViewModel : INotifyPropertyChanged
    {
        public string Receipt { get; set; }

        public CheckoutViewModel()
        {
            Receipt = ShoppingCartService.Current.GenerateReceipt();
        }

        public void RefreshReceipt()
        {
            Receipt = ShoppingCartService.Current.GenerateReceipt();
            OnPropertyChanged(nameof(Receipt));
        }

        public void Finalize()
        {
            ShoppingCartService.Current.FinalizeCheckout();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
