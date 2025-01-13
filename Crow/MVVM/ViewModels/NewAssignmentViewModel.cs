using System.Collections.ObjectModel;
using System.Windows.Input;
using Crow.MVVM.Models;

namespace Crow.ViewModels;

public class NewAssignmentViewModel : BaseViewModel
{
    public string Title { get; set; }
    public string PhotoPath { get; set; }

    public ICommand SaveAssignmentCommand { get; }

    public NewAssignmentViewModel()
    {
        SaveAssignmentCommand = new Command(SaveAssignment);
    }

    private async void SaveAssignment()
    {
        if (string.IsNullOrWhiteSpace(Title) || string.IsNullOrWhiteSpace(PhotoPath))
        {
            await App.Current.MainPage.DisplayAlert("Error", "Please fill in all fields.", "OK");
            return;
        }

        // Create the new assignment
        var newAssignment = new Assignment
        {
            Title = Title,
            PhotoPath = PhotoPath,
            Comments = new ObservableCollection<string>(),
            Likes = 0
        };

        // Save to the database
        App.DatabaseService.SaveAssignment(newAssignment);

        // Navigate back to the AssignmentPage
        await App.Current.MainPage.Navigation.PopAsync();
    }
}
