using Microsoft.Maui.Controls;
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
}
