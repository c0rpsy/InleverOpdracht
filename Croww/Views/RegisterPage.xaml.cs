namespace Croww.Views;

public partial class RegisterPage : ContentPage
{
    public RegisterPage()
    {
        InitializeComponent();
        BindingContext = new Croww.ViewModels.RegisterViewModel();
    }

    private async void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Success", "Account created", "OK");
        await Navigation.PushAsync(new Croww.Views.LoginPage());
    }

}