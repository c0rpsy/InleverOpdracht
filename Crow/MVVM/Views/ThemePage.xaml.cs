using Crow.MVVM.ViewModels;
using Crow.ViewModels;

namespace Crow.MVVM.Views;

public partial class ThemePage : ContentPage
{
    public ThemePage()
    {
        InitializeComponent();

        // Makes it so the backend basically knows about the frontend
        BindingContext = new ThemeViewModel();
    }
}
