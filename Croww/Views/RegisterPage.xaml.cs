namespace Croww.Views;

public partial class RegisterPage : ContentPage
{
    public RegisterPage()
    {
        InitializeComponent();
        BindingContext = new Croww.ViewModels.RegisterViewModel();
    }
}