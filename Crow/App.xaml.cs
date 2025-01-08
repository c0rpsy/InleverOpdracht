using Microsoft.Extensions.DependencyInjection;

namespace Crow;

public partial class App : Application
{
    public IServiceProvider Services { get; }

    public App(IServiceProvider services)
    {
        InitializeComponent();

        Services = services;

        MainPage = new NavigationPage(new MainPage());
    }
}