using Microsoft.Maui.Controls;
using System;
   
namespace Croww.Views
{
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        private async void OnEditProfilePictureClicked(object sender, EventArgs e)
        {
            try
            {
                var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions { Title = "Pick a Profile Picture" });
                if (result != null)
                {
                    var stream = await result.OpenReadAsync();
                    ProfileImage.Source = ImageSource.FromStream(() => stream);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Unable to pick an image: " + ex.Message, "OK");
            }
        }
    }
}
