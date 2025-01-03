using Croww.Models;
using Microsoft.Maui.Controls;
using System;

namespace Croww.Views
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            if (PasswordEntry.Text != ConfirmPasswordEntry.Text)
            {
                Notification.Text = "Passwords do not match.";
                return;
            }

            var user = new UserProfile
            {
                Username = UsernameEntry.Text?.Trim(),
                Email = EmailEntry.Text?.Trim(),
                Password = PasswordEntry.Text?.Trim()
            };

            bool isRegistered = App.DatabaseService.RegisterUser(user);
            if (isRegistered)
            {
                Notification.TextColor = Colors.Green;
                Notification.Text = "Registration successful! Please log in.";
                await Navigation.PushAsync(new LoginPage());
            }
            else
            {
                Notification.Text = "Email already exists. Please use a different email.";
            }
        }
    }
}
