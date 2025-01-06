namespace Croww.Views;

public partial class ThemePage : ContentPage
{
    public ThemePage()
    {
        InitializeComponent();
    }
    private async void Theme_Clicked(object sender, EventArgs e)
    {
        // Get the theme name from the button (or assign it directly)
        var themeName = (sender as ImageButton)?.CommandParameter?.ToString();

        if (!string.IsNullOrEmpty(themeName))
        {
            await Navigation.PushAsync(new AssignmentPage(themeName));
        }
    }
}