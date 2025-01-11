using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui;
using Crow.MVVM.Models.Services;

namespace Crow;

public partial class App : Application
{
    public IServiceProvider Services { get; }
    public static DatabaseService DatabaseService { get; private set; }


    public App(IServiceProvider services, string dbPath)
    {
        InitializeComponent();

        Services = services;

        // Database service
        DatabaseService = new DatabaseService(dbPath);


        MainPage = new NavigationPage(new MainPage());
    }
}