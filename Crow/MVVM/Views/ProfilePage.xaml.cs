using Crow.MVVM.ViewModels;
using Crow.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Crow.MVVM.Views;

public partial class ProfilePage : ContentPage
{
    public ProfilePage()
    {
        InitializeComponent();

        var userRepository = ((App)Application.Current).Services.GetService<IUserRepository>();
        BindingContext = new ProfileViewModel(userRepository);
    }
}