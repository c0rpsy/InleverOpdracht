using System.Collections.ObjectModel;
using System.Windows.Input;
using Crow.MVVM.Models; // Ensure this using directive is included
using Crow.MVVM.Views;
using Microsoft.Maui.Controls;

namespace Crow.MVVM.ViewModels
{
    public class AssignmentViewModel : BaseViewModel
    {
        public ObservableCollection<Assignment> Assignments { get; set; }

        public ICommand LikeCommand { get; }
        public ICommand PostCommentCommand { get; }

        public ICommand NavigateToNewAssignmentCommand { get; }

        public AssignmentViewModel()
        {
            Assignments = new ObservableCollection<Assignment>();

            var initialAssignment = new Assignment
            {
                Title = "Nature Walk",
                PhotoPath = "nature_image.png",
                Comments = new ObservableCollection<string>(),
                Likes = 0
            };

            Assignments.Add(initialAssignment);

            LikeCommand = new Command<Assignment>(LikePhoto);
            PostCommentCommand = new Command<Assignment>(PostComment);
            NavigateToNewAssignmentCommand = new Command(async () => await App.Current.MainPage.Navigation.PushAsync(new NewAssignmentPage()));
        }



        private void LikePhoto(Assignment assignment)
        {
            if (assignment == null) return;

            assignment.Likes++;
            OnPropertyChanged(nameof(Assignments));
        }

        private void PostComment(Assignment assignment)
        {
            if (assignment == null || string.IsNullOrWhiteSpace(assignment.NewComment)) return;

            assignment.Comments.Add(assignment.NewComment);
            assignment.NewComment = string.Empty; // Clear the input field
            OnPropertyChanged(nameof(Assignments));
        }
    }
}
