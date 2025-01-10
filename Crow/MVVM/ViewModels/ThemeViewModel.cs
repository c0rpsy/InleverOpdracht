using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace Croww.ViewModels
{
    public class ThemeViewModel : BaseViewModel
    {
        public ObservableCollection<Theme> Themes { get; set; }

        public ICommand ThemeSelectedCommand { get; }

        public ThemeViewModel()
        {
            // Load the themes
            LoadThemes();

            // Command for when a theme is selected
            ThemeSelectedCommand = new Command<Theme>(OnThemeSelected);
        }

        private void LoadThemes()
        {
            // Fetch themes from the database or any data source
            Themes = new ObservableCollection<Theme>(App.DatabaseService.GetThemes());
        }

        private async void OnThemeSelected(Theme selectedTheme)
        {
            if (selectedTheme != null)
            {
                // Navigate to AssignmentPage with the selected theme
                await App.Current.MainPage.Navigation.PushAsync(new AssignmentPage(selectedTheme.Name));
            }
        }
    }

    public class Theme
    {
        public string Name { get; set; }
        public string IconPath { get; set; } // Example for additional properties
    }
}
