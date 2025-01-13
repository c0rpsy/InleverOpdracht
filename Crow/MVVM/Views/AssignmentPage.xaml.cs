using Crow.MVVM.ViewModels;
using Crow.MVVM.Models;
namespace Crow.MVVM.Views;

public partial class AssignmentPage : ContentPage
{

    public AssignmentPage()
    {
        InitializeComponent();

        BindingContext = new AssignmentViewModel();

    }
}
