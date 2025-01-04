using Croww.Views;
using System.IO;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration;
using AndroidSpecific = Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;

namespace Croww
{
    public partial class App : Application
    {
        public static DatabaseService DatabaseService { get; private set; }

        public App()
        {
            InitializeComponent();

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "UserProfile.db");
            Console.WriteLine($"Database Path: {dbPath}");

            DatabaseService = new DatabaseService(dbPath);

            MainPage = new NavigationPage(new MainPage());

            // Pins the dashboard page to the bottom of the page. 
            // AndroidSpecific.TabbedPage.SetToolbarPlacement(this, AndroidSpecific.ToolbarPlacement.Bottom);

        }
    }
}
