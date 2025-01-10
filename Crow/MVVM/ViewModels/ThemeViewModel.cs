using System.Collections.ObjectModel;
using System.Windows.Input;
using Crow;
using Crow.MVVM.Models;
using Crow.MVVM.Views;
using Microsoft.Maui.Controls;

namespace Crow.ViewModels
{
    public class ThemeViewModel : BaseViewModel
    {
        public ObservableCollection<Theme> Themes { get; set; }

        public ICommand NavigateToAssignmentCommand { get; }

        public ThemeViewModel()
        {
            // Initialize the six predetermined themes
            Themes = new ObservableCollection<Theme>
            {
                new Theme { Name = "Pets", IconPath = "pet_image.png" },
                new Theme { Name = "Nature", IconPath = "nature_image.png" },
                new Theme { Name = "Flowers", IconPath = "flower_image.png" },
                new Theme { Name = "Macro", IconPath = "macro_image.png" },
                new Theme { Name = "People", IconPath = "people_image.png" },
                new Theme { Name = "Travel", IconPath = "travel_image.png" },
            };

            // Command for navigating to assignment page
            NavigateToAssignmentCommand = new Command<Theme>(async (theme) => await NavigateToAssignment(theme));
        }

        private async Task NavigateToAssignment(Theme selectedTheme)
        {
            if (selectedTheme == null) return;

            // Navigate to the AssignmentPage with the selected theme
            await App.Current.MainPage.Navigation.PushAsync(new AssignmentPage(selectedTheme.Name));
        }
    }
}


