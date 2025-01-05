using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System;
using System.IO;
using System.Threading.Tasks;
using Croww.Models;
using Microsoft.Maui;


namespace Croww.Views
{
    public partial class ProfilePage : ContentPage
    {
        private UserProfile _userProfile;

        public ProfilePage()
        {
            InitializeComponent();

            // Load the logged-in user's profile
            var userId = App.CurrentUserId;
            _userProfile = App.DatabaseService.GetUserProfile(userId);

            if (_userProfile != null)
            {
                LoadUserProfile();
            }
            else
            {
                Console.WriteLine($"Failed to load profile for user ID {userId}.");
            }
        }

        private async void OnEditProfilePictureClicked(object sender, EventArgs e)
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = "Please select a profile picture"
            });

            if (result != null)
            {
                // Save the selected image to local storage
                var newFilePath = Path.Combine(FileSystem.AppDataDirectory, result.FileName);
                using (var stream = await result.OpenReadAsync())
                using (var newStream = File.OpenWrite(newFilePath))
                {
                    await stream.CopyToAsync(newStream);
                }

                // Update the profile image source
                ProfileImage.Source = newFilePath;

                // Save the new file path to the database or local storage
                SaveProfileImagePath(newFilePath);
            }
        }

        private void SaveProfileImagePath(string newFilePath)
        {
            // Get the current user's profile using the globally stored user ID
            var userProfile = App.DatabaseService.GetUserProfile(App.CurrentUserId);

            if (userProfile != null)
            {
                // Update the profile image path
                userProfile.ProfileImagePath = newFilePath;

                // Save the updated user profile to the database
                App.DatabaseService.SaveUserProfile(userProfile);

                Console.WriteLine($"Profile image path saved to the database for user ID {App.CurrentUserId}.");
            }
            else
            {
                Console.WriteLine("Failed to retrieve the logged-in user's profile.");
            }
        }

        private void LoadUserProfile()
        {
            if (_userProfile != null)
            {
                // Assuming you have a Label for the username and Editor for bio
                // UsernameLabel.Text = _userProfile.Username;
                // BioEditor.Text = _userProfile.Bio;

                // Set the profile image if available
                if (!string.IsNullOrEmpty(_userProfile.ProfileImagePath))
                {
                    ProfileImage.Source = ImageSource.FromFile(_userProfile.ProfileImagePath);
                }
            }
        }

        private void OnSaveChangesClicked(object sender, EventArgs e)
        {
            if (_userProfile != null)
            {
                // Update bio and save to database
                _userProfile.Bio = BioEditor.Text?.Trim();
                App.DatabaseService.SaveUserProfile(_userProfile);
                DisplayAlert("Profile Saved", "Your changes have been saved.", "OK");
            }
            else
            {
                Console.WriteLine("Failed to save changes: UserProfile is null.");
            }
        }

    }
}

