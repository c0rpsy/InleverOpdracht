using Crow.MVVM.Views;
using SQLite;
using Crow.MVVM.Models;

namespace Crow.MVVM.Models.Services
{
    public class DatabaseService
    {
        private SQLiteConnection _database;

        public DatabaseService(string dbPath)
        {
            Console.WriteLine($"Database Path: {dbPath}"); // Log database path
            _database = new SQLiteConnection(dbPath);

            _database.CreateTable<Theme>();
            _database.CreateTable<Assignment>(); // Ensure the Assignment table is created
            _database.CreateTable<UserProfile>(); // Ensure table creation
        }

        public List<Theme> GetThemes()
        {
            return _database.Table<Theme>().ToList();
        }

        public void SaveTheme(Theme theme)
        {
            _database.Insert(theme);
        }

        public void DeleteTheme(Theme theme)
        {
            _database.Delete(theme);
        }

        public List<Assignment> GetAssignmentsByTheme(string themeName)
        {
            return _database.Table<Assignment>().Where(a => a.Theme == themeName).ToList();
        }

        public void SavePhotoPathForAssignment(int assignmentId, string photoPath)
        {
            var assignment = _database.Table<Assignment>().FirstOrDefault(a => a.Id == assignmentId);
            if (assignment != null)
            {
                assignment.PhotoPath = photoPath;
                _database.Update(assignment);
            }
        }

        public async Task<bool> RegisterUser(UserProfile user)
        {
            return await Task.Run(() =>
            {
                // Check if the email already exists
                var existingUser = _database.Table<UserProfile>().FirstOrDefault(u => u.Email == user.Email);
                if (existingUser != null)
                {
                    return false; // Email already exists
                }

                // Insert the new user
                _database.Insert(user);
                return true;
            });
        }


        public UserProfile AuthenticateUser(string username, string password)
        {
            // Check if a user exists with the given username and password
            return _database.Table<UserProfile>().FirstOrDefault(u => u.Username == username && u.Password == password);
        }

        public async Task<bool> UpdateUserBio(string username, string newBio)
        {
            return await Task.Run(() =>
            {
                // Fetch the user by username
                var user = _database.Table<UserProfile>().FirstOrDefault(u => u.Username == username);

                if (user != null)
                {
                    // Update the bio
                    user.Bio = newBio;
                    _database.Update(user);
                    return true;
                }

                return false; // User not found
            });
        }
        public async Task<bool> UpdateUserProfilePicture(string username, string newProfileImagePath)
        {
            return await Task.Run(() =>
            {
                // Fetch the user by username
                var user = _database.Table<UserProfile>().FirstOrDefault(u => u.Username == username);

                if (user != null)
                {
                    // Update the profile image path
                    user.ProfileImagePath = newProfileImagePath;
                    _database.Update(user);
                    return true;
                }

                return false; // User not found
            });
        }


    }
}
