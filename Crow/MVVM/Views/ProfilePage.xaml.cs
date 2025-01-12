using Crow.MVVM.ViewModels;
using Crow.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Crow.MVVM.Views;

public partial class ProfilePage : ContentPage
{
    public ProfilePage()
    {
        InitializeComponent();

        var userRepository = ((App)Application.Current).Services.GetService<IUserRepository>();
        BindingContext = new ProfileViewModel(userRepository);
    }

    private async void OnFlyoutMenuClicked(object sender, EventArgs e)
    {
        var action = await DisplayActionSheet("Options", "Cancel", null,"Change Bio", "Change Profile picture", "Log Out");

        switch (action)
        {
            case "Log Out":
                await HandleLogOut();
                break;

            case "Change Bio":
                await HandleChangeBio();
                break;
            case "Change Profile picture":
                await HandleChangeProfilepic();
                break;
        }
    }
    private async Task HandleChangeProfilepic()
    {
        // Open the gallery to pick a photo
        var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
        {
            Title = "Select Profile Picture"
        });

        if (result != null)
        {
            var viewModel = BindingContext as ProfileViewModel;

            if (viewModel != null)
            {
                // Save the photo path to the ViewModel
                viewModel.ProfileImagePath = result.FullPath;

                // Save the updated profile picture path to the database
                bool isSaved = await App.DatabaseService.UpdateUserProfilePicture(viewModel.Username, result.FullPath);

                if (isSaved)
                {
                    await DisplayAlert("Success", "Your profile picture has been updated.", "OK");
                }
                else
                {
                    await DisplayAlert("Error", "Failed to update your profile picture. Please try again.", "OK");
                }
            }
        }
    }

    private async Task HandleLogOut()
    {
        // Perform log-out logic
        await DisplayAlert("Log Out", "You have been logged out.", "OK");
        Application.Current.MainPage = new NavigationPage(new MainPage());
    }

    private async Task HandleChangeBio()
    {
        // Prompt user to enter a new bio
        string newBio = await DisplayPromptAsync("Change Bio", "Enter your new bio:");

        if (!string.IsNullOrWhiteSpace(newBio))
        {
            var viewModel = BindingContext as ProfileViewModel;

            if (viewModel != null)
            {
                // Update the bio in the ViewModel
                viewModel.Bio = newBio;

                // Save the updated bio to the database
                bool isSaved = await App.DatabaseService.UpdateUserBio(viewModel.Username, newBio);

                if (isSaved)
                {
                    await DisplayAlert("Success", "Your bio has been updated.", "OK");
                }
                else
                {
                    await DisplayAlert("Error", "Failed to update your bio. Please try again.", "OK");
                }
            }
        }
    }

}