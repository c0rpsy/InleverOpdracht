using Crow.MVVM.ViewModels;
using Crow.MVVM.Models;
namespace Crow.MVVM.Views;

public partial class AssignmentPage : ContentPage
{

    private List<string> selectedThemes;

    public AssignmentPage(List<string> themes)
    {
        InitializeComponent();
        selectedThemes = themes; // Assign the passed themes to the local variable

        // Set the ViewModel's themes and load assignments
        var viewModel = BindingContext as AssignmentViewModel;
        if (viewModel != null)
        {
            viewModel.SelectedThemes = selectedThemes;
            viewModel.LoadAssignments();
        }
    }
}
