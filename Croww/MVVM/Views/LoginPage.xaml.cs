using Croww.MVVM.ViewModels;

namespace Croww.MVVM.Views;

public partial class LoginPage : ContentPage
{
    //private const string HardcodedUsername = "admin";
    //private const string HardcodedPassword = "password123";

    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        // Trim the inputs to avoid issues with leading/trailing spaces
        // Just in case they hit space as a habit (like me)
        var username = UsernameEntry.Text?.Trim();
        var password = PasswordEntry.Text?.Trim();

        // Checks if the fields are filled
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Fields can't be empty", "Please enter both username and password.", "OK");
            return;
        }

        // Authenticate that user exists
        var user = App.DatabaseService.AuthenticateUser(username, password);
        if (user != null)
        {
            // Store the logged-in user's ID globally
            App.CurrentUserId = user.Id;
            // Successful login
            await Navigation.PushAsync(new DashboardPage());
        }
        else
        {
            // Failed login
            await DisplayAlert("Login Failed", "Invalid username or password.", "OK");
        }
    }

}
