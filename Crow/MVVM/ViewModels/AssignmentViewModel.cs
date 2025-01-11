using System.Collections.ObjectModel;
using System.Windows.Input;
using Crow.MVVM.Models; // Ensure this using directive is included
using Crow.MVVM.Views;
using Microsoft.Maui.Controls;

namespace Crow.MVVM.ViewModels
{
    public class AssignmentViewModel : BaseViewModel
    {
        private string _themeName;

        public ObservableCollection<Assignment> Assignments { get; private set; }
        public string ThemeName
        {
            get => _themeName;
            set
            {
                _themeName = value;
                OnPropertyChanged();
                LoadAssignments();
            }
        }

        public ICommand TakePhotoCommand { get; }

        public AssignmentViewModel()
        {
            Assignments = new ObservableCollection<Assignment>();
            TakePhotoCommand = new Command<int>(async (assignmentId) => await TakePhoto(assignmentId));
        }

        private void LoadAssignments()
        {
            // Fetch assignments from the database
            var assignments = App.DatabaseService.GetAssignmentsByTheme(ThemeName);

            Assignments.Clear();
            foreach (var assignment in assignments)
            {
                Assignments.Add(assignment);
            }
        }

        private async Task TakePhoto(int assignmentId)
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();

                if (photo != null)
                {
                    var photoPath = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);

                    using (var stream = await photo.OpenReadAsync())
                    using (var fileStream = File.OpenWrite(photoPath))
                    {
                        await stream.CopyToAsync(fileStream);
                    }

                    // Update database with the photo path
                    App.DatabaseService.SavePhotoPathForAssignment(assignmentId, photoPath);

                    // Update the UI
                    var assignment = Assignments.FirstOrDefault(a => a.Id == assignmentId);
                    if (assignment != null)
                    {
                        assignment.PhotoPath = photoPath;
                        OnPropertyChanged(nameof(Assignments));
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., user canceled photo)
                Console.WriteLine($"Error taking photo: {ex.Message}");
            }
        }
    }
}
