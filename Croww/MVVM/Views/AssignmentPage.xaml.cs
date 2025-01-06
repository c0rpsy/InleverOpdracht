using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System;
using System.Collections.ObjectModel;
using Croww.Models;

namespace Croww.Views
{
    public partial class AssignmentPage : ContentPage
    {
        public string ThemeName { get; set; }
        public ObservableCollection<Assignment> Assignments { get; set; }

        public AssignmentPage(string themeName)
        {
            InitializeComponent();
            ThemeName = themeName;

            // Bind the theme name and load assignments
            BindingContext = this;
            LoadAssignments();
        }

        private void LoadAssignments()
        {
            // Fetch assignments linked to the theme from the database
            Assignments = new ObservableCollection<Assignment>(
                App.DatabaseService.GetAssignmentsByTheme(ThemeName)
            );
        }

        private async void TakePhoto_Clicked(object sender, EventArgs e)
        {
            var assignmentId = (int)((Button)sender).CommandParameter;

            try
            {
                // Use the camera to take a photo
                var photo = await MediaPicker.CapturePhotoAsync();

                if (photo != null)
                {
                    var photoPath = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);
                    using (var stream = await photo.OpenReadAsync())
                    using (var fileStream = File.OpenWrite(photoPath))
                    {
                        await stream.CopyToAsync(fileStream);
                    }

                    // Save the photo path in the database
                    App.DatabaseService.SavePhotoPathForAssignment(assignmentId, photoPath);

                    // Update the UI
                    var assignment = Assignments.FirstOrDefault(a => a.Id == assignmentId);
                    if (assignment != null)
                    {
                        assignment.PhotoPath = photoPath;
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Unable to take photo: " + ex.Message, "OK");
            }
        }
    }
}
