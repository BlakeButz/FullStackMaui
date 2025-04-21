using Maui.eCommerce.ViewModels;
using Library.eCommerce.Services;

namespace Maui.eCommerce.Views;

public partial class CheckoutView : ContentPage
{
    public CheckoutView()
    {
        InitializeComponent();
        BindingContext = new CheckoutViewModel();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        (BindingContext as CheckoutViewModel)?.RefreshReceipt();
    }

    private void GoBackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void FinalizeClicked(object sender, EventArgs e)
    {
        (BindingContext as CheckoutViewModel)?.Finalize();
        System.Environment.Exit(0);
    }
}