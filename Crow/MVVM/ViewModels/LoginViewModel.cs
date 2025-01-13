using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Crow.MVVM.Views;

namespace Crow.MVVM.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _password;
        private string _statusMessage;
        private bool _isStatusMessageVisible;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }
        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged();
            }
        }
        public bool IsStatusMessageVisible
        {
            get => _isStatusMessageVisible;
            set
            {
                _isStatusMessageVisible = value;
                OnPropertyChanged();
            }
        }
        public ICommand LoginCommand { get; }
        public LoginViewModel()
        {
            LoginCommand = new Command(OnLogin);
        }
        private async void OnLogin()
        {
            // Validate user input
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Please enter both username and password.", "OK");
                return;
            }

            // Authenticate user
            var user = App.DatabaseService.AuthenticateUser(Username, Password);

            if (user != null)
            {
                // Login successful, navigate to DashboardPage
                App.Current.MainPage = new NavigationPage(new DashboardPage());
            }
            else
            {
                // Login failed, show error
                await App.Current.MainPage.DisplayAlert("Error", "Invalid username or password.", "OK");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
