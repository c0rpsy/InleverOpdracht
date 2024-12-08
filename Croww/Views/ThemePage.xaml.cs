namespace Croww.Views;

public partial class ThemePage : ContentPage
{
    public ThemePage()
    {
        InitializeComponent();
    }

    private async void Pets_Clicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Theme selection", "Do you want to choose this theme?", "Yes", "No");

        if (answer)
        {
            // Show success message
            await DisplayAlert("Success", "You have successfully chosen this theme!", "OK");
        }
        else
        {
            // Simply close the dialog without further action
            // Optionally show a message or log
            Console.WriteLine("Action cancelled by the user.");
        }

    }
}