using Crow.MVVM.Views;
using SQLite;
using Crow.MVVM.Models;
using System.Collections.ObjectModel;

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
            _database.CreateTable<UserProfile>(); // Ensure user table creation
            _database.CreateTable<UserTheme>(); // Create UserThemes table

            SeedThemes();
        }

        public void SaveTheme(Theme theme)
        {
            _database.Insert(theme);
        }

        public void DeleteTheme(Theme theme)
        {
            _database.Delete(theme);
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

        public async Task AddThemeToUser(string username, string themeName)
        {
            await Task.Run(() =>
            {
                var userTheme = new UserTheme
                {
                    Username = username,
                    ThemeName = themeName
                };

                // Check if the theme is already associated with the user
                var existing = _database.Table<UserTheme>().FirstOrDefault(ut => ut.Username == username && ut.ThemeName == themeName);
                if (existing == null)
                {
                    _database.Insert(userTheme);
                    Console.WriteLine($"Theme '{themeName}' added for user '{username}'.");
                }
                else
                {
                    Console.WriteLine($"Theme '{themeName}' is already associated with user '{username}'.");
                }
            });
        }


        public async Task RemoveThemeFromUser(string username, string themeName)
        {
            await Task.Run(() =>
            {
                var userTheme = _database.Table<UserTheme>().FirstOrDefault(ut => ut.Username == username && ut.ThemeName == themeName);
                if (userTheme != null)
                {
                    _database.Delete(userTheme);
                }
            });
        }

        public List<string> GetUserThemes(string username)
        {
            return _database.Table<UserTheme>()
                            .Where(ut => ut.Username == username)
                            .Select(ut => ut.ThemeName)
                            .ToList();
        }

        public List<Theme> GetThemes()
        {
            return _database.Table<Theme>().ToList();
        }

        public void AddTheme(Theme theme)
        {
            // Prevent duplicates by checking if the theme already exists
            if (!_database.Table<Theme>().Any(t => t.Name == theme.Name))
            {
                _database.Insert(theme);
            }
        }
        private void SeedThemes()
        {
            var presetThemes = new List<Theme>
    {
        new Theme { Name = "Pets", IconPath = "pet_image.png" },
        new Theme { Name = "Nature", IconPath = "nature_image.png" },
        new Theme { Name = "Flowers", IconPath = "flower_image.png" },
        new Theme { Name = "Macro", IconPath = "macro_image.png" },
        new Theme { Name = "People", IconPath = "people_image.png" },
        new Theme { Name = "Travel", IconPath = "travel_image.png" }
    };

            var existingThemes = GetThemes();

            if (!existingThemes.Any())
            {
                foreach (var theme in presetThemes)
                {
                    AddTheme(theme);
                }
            }
        }
        public void ResetThemeTable()
        {
            _database.DropTable<Theme>();
            _database.CreateTable<Theme>();
            SeedThemes();
        }

        public void SaveAssignment(Assignment assignment)
        {
            _database.Insert(assignment);
            Console.WriteLine($"Assignment '{assignment.Title}' saved to the database.");
        }


    }
}
