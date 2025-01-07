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
            LoginCommand = new Command(ExecuteLogin);
        }
        private void ExecuteLogin()
        {
            // Validate the username and password
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                StatusMessage = "Please enter a username and password.";
                IsStatusMessageVisible = true;
                return;
            }
            // Check if the username and password are correct
            if (Username == "admin" && Password == "admin")
            {
                // Navigate to the home page
                App.Current.MainPage = new HomePage();
            }
            else
            {
                StatusMessage = "Invalid username or password.";
                IsStatusMessageVisible = true;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
