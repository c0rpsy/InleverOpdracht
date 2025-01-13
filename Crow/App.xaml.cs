using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui;
using Crow.MVVM.Models.Services;
using Crow.MVVM.Models;

namespace Crow;
public partial class App : Application
{
    public IServiceProvider Services { get; }
    public static DatabaseService DatabaseService { get; private set; }
    public static UserProfile LoggedInUser { get; set; } // Holds the logged-in user


    public App(IServiceProvider services, string dbPath)
    {
        InitializeComponent();

        Services = services;

        // Initialize DatabaseService
        DatabaseService = new DatabaseService(dbPath);

        MainPage = new NavigationPage(new MainPage());
    }
}
