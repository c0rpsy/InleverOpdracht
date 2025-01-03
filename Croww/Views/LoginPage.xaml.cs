using Croww.ViewModels;

namespace Croww.Views;

public partial class LoginPage : ContentPage
{
    private const string HardcodedUsername = "admin";
    private const string HardcodedPassword = "password123";

    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        // Trim the inputs to avoid issues with leading/trailing spaces
        var username = UsernameEntry.Text?.Trim();
        var password = PasswordEntry.Text?.Trim();

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Error", "Please enter both username and password.", "OK");
            return;
        }

        // Authenticate the user
        var user = App.DatabaseService.AuthenticateUser(username, password);
        if (user != null)
        {
            // Successful login
            await DisplayAlert("Login Success", $"Welcome, {user.Username}!", "OK");
            await Navigation.PushAsync(new ProfilePage());
        }
        else
        {
            // Failed login
            await DisplayAlert("Login Failed", "Invalid username or password.", "OK");
        }
    }

}
