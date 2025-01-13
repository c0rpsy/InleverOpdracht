using System.Collections.ObjectModel;
using System.Windows.Input;
using Crow.MVVM.Views;
using Crow.MVVM.Models;

namespace Crow.ViewModels;

public class ThemeViewModel : BaseViewModel
{
    public ObservableCollection<Theme> Themes { get; set; }
    public ICommand SelectThemeCommand { get; }
    public ICommand SeeAssignmentsCommand { get; }

    public ThemeViewModel()
    {
        // Load themes from the database
        LoadThemes();

        // Command for selecting a theme
        SelectThemeCommand = new Command<Theme>(async (theme) => await SelectTheme(theme));

        // Command for seeing assignments
        SeeAssignmentsCommand = new Command(async (Assignment) => await SeeAssignments());
    }

    private void LoadThemes()
    {
        // Load all themes from the database
        var themesFromDatabase = App.DatabaseService.GetThemes();

        // Initialize Themes ObservableCollection
        Themes = new ObservableCollection<Theme>(themesFromDatabase);
    }

    private async Task SelectTheme(Theme selectedTheme)
    {
        if (selectedTheme == null) return;

        string username = "current_user"; // Replace with actual logged-in user logic

        // Add the selected theme to the user's profile in the database
        await App.DatabaseService.AddThemeToUser(username, selectedTheme.Name);

        // Optionally, navigate to another page or display a success message
        await App.Current.MainPage.DisplayAlert("Theme Selected", $"{selectedTheme.Name} has been selected.", "OK");
    }

    private async Task SeeAssignments()
    {
        var selectedThemes = Themes.Select(t => t.Name).ToList();
        var mainPage = App.Current?.Windows[0]?.Page;
        if (mainPage != null)
        {
            await mainPage.Navigation.PushAsync(new AssignmentPage());
        }
    }
}
