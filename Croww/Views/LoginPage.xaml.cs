using Croww.ViewModels;

namespace Croww.Views;

public partial class LoginPage : ContentPage
{
    private const string HardcodedUsername = "admin";
    private const string HardcodedPassword = "password123";

    public LoginPage()
    {
        InitializeComponent();
        BindingContext = new LoginViewModel();
    }

    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        string username = UsernameEntry.Text;
        string password = PasswordEntry.Text;

        if (username == HardcodedUsername && password == HardcodedPassword)
        {
            // Navigate to HomePage
            await Navigation.PushAsync(new DashboardPage());
        }
        else
        {
            await DisplayAlert("Error", "Invalid username or password", "OK");
        }
    }
}
