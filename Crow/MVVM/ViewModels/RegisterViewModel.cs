using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Crow.MVVM.Models;
using Crow.Interfaces;
using Crow.MVVM.Views;
using Crow.Repositories;

namespace Crow.MVVM.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        public ICommand RegisterCommand { get; }

        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public RegisterViewModel()
        {
            RegisterCommand = new Command(OnRegister);
        }

        private async void OnRegister()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Please fill in all fields.", "OK");
                return;
            }

            if (Password != ConfirmPassword)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Passwords do not match.", "OK");
                return;
            }

            var user = new UserProfile
            {
                Username = Username.Trim(),
                Email = Email.Trim(),
                Password = Password.Trim()
            };

            bool isRegistered = await App.DatabaseService.RegisterUser(user);

            if (isRegistered)
            {
                await App.Current.MainPage.DisplayAlert("Success", "Registration successful!", "OK");

                // Redirect to DashboardPage
                await App.Current.MainPage.Navigation.PushAsync(new DashboardPage());
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Email already exists.", "OK");
            }
        }

    }
}