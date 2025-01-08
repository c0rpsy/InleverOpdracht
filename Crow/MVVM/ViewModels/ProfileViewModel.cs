using System.Threading.Tasks;
using System.Windows.Input;
using Crow.MVVM.Models;
using Crow.Repositories;
using Crow.Interfaces;
using Microsoft.Maui.Controls;

namespace Crow.MVVM.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        // Properties for binding
        private string _username;
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        private string _bio;
        public string Bio
        {
            get => _bio;
            set => SetProperty(ref _bio, value);
        }

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _profileImagePath;
        public string ProfileImagePath
        {
            get => _profileImagePath;
            set => SetProperty(ref _profileImagePath, value);
        }

        public ICommand SaveProfileCommand { get; }
        public ICommand LoadProfileCommand { get; }

        private readonly IUserRepository _userRepository;

        // Constructor
        public ProfileViewModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            // Initialize commands
            SaveProfileCommand = new Command(async () => await SaveProfileAsync());
            LoadProfileCommand = new Command(async () => await LoadProfileAsync());

            // Auto-load the profile
            _ = LoadProfileAsync();
        }

        // Load Profile
        private async Task LoadProfileAsync()
        {
            var user = await _userRepository.GetUserProfileAsync();
            if (user != null)
            {
                Username = user.Username;
                Bio = user.Bio;
                Email = user.Email;
                ProfileImagePath = user.ProfileImagePath;
            }
        }

        // Save Profile
        private async Task SaveProfileAsync()
        {
            var userProfile = new UserProfile
            {
                Username = Username,
                Bio = Bio,
                Email = Email,
                ProfileImagePath = ProfileImagePath
            };

            await _userRepository.SaveUserProfileAsync(userProfile);
        }
    }
}
