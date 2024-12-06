using Croww.Views;

namespace Croww
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            // Navigate to LoginPage
            await Navigation.PushAsync(new LoginPage());
        }

        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            // Navigate to RegisterPage
            await Navigation.PushAsync(new RegisterPage());
        }
    }

}