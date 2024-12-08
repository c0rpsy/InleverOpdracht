namespace Crow.Views;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
        BindingContext = new Crow.ViewModels.RegisterViewModel();
    }
}