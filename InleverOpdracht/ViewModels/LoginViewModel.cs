using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using InleverOpdracht.Pages;

namespace InleverOpdracht.ViewModels
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
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                StatusMessage = "Username and Password cannot be empty.";
                IsStatusMessageVisible = true;
                return;
            }

            // Simulate a login process
            if (Username == "User" && Password == "password")
            {
                StatusMessage = "Login successful!";
                IsStatusMessageVisible = true;
            }
            else if (Username == _username && Password == _password)
            {
                StatusMessage = "Login successful!";
                IsStatusMessageVisible = true;
            }
            else
            {
                StatusMessage = "Invalid Username or Password.";
                IsStatusMessageVisible = true;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
