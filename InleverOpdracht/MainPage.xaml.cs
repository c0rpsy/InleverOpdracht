using InleverOpdracht.Pages;

namespace InleverOpdracht
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            // Navigate to LoginPage
            await Navigation.PushAsync(new LoginPage());
        }
    }

}
