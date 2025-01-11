using Crow.MVVM.ViewModels;
namespace Crow.MVVM.Views;

public partial class AssignmentPage : ContentPage
{
    public AssignmentPage(string themeName)
    {
        InitializeComponent();

        if (BindingContext is AssignmentViewModel viewModel)
        {
            viewModel.ThemeName = themeName;
        }
    }
}
