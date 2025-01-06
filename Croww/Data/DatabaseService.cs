using SQLite;
using Croww.Models;
using System.Linq;

public class DatabaseService
{
    private readonly SQLiteConnection _database;

    public DatabaseService(string dbPath)
    {
        _database = new SQLiteConnection(dbPath);
        // Tables
        _database.CreateTable<UserProfile>();
        _database.CreateTable<Assignment>();
        // Seeding
        SeedDefaultAssignments();
    }

    // Users
    public bool RegisterUser(UserProfile user)
    {
        if (_database.Table<UserProfile>().Any(u => u.Email == user.Email))
            return false; // Email already exists

        _database.Insert(user);
        return true;
    }

    public UserProfile AuthenticateUser(string username, string password)
    {
        return _database.Table<UserProfile>().FirstOrDefault(u => u.Username == username && u.Password == password);
    }

    public bool UpdateUserProfile(UserProfile user)
    {
        var existingUser = _database.Table<UserProfile>().FirstOrDefault(u => u.Id == user.Id);
        if (existingUser == null)
            return false; // User not found

        _database.Update(user);
        return true;
    }

    public UserProfile GetUserProfile(int id)
    {
        return _database.Table<UserProfile>().FirstOrDefault(u => u.Id == id);
    }

    public void SaveUserProfile(UserProfile userProfile)
    {
        _database.InsertOrReplace(userProfile);
    }

    // Themes and Assignments
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

    public void SaveAssignment(Assignment assignment)
    {
        _database.Insert(assignment);
    }

    // Seeding for default assignments for a theme. 
    private void SeedDefaultAssignments()
    {
        var themesWithDefault = new Dictionary<string, List<string>>
        {
            { "Pets", new List<string> { "Take a photo of a dog", "Take a photo of a cat", "Take a photo of your favorite animal", "Take a photo of an exotic animal" } },
            { "Nature", new List<string> { "Take a photo of a tree", "Take a photo of a sunset", "Take a photo of a cloud", "Take a photo of a cool rock" } },
            { "Flower", new List<string> { "Take a photo of a rose", "Take a photo of a field of flowers", "Take a photo of your favorite flower", "Take a photo of a blue flower" } },
            { "Macro", new List<string> { "Take a close-up of a leaf", "Take a close-up of a bug", "Take a close-up of your hand", "Take a close-up of a drink" } },
            { "People", new List<string> { "Take a portrait photo", "Take a candid photo of someone", "Take a sideprofile of someone", "Take a photo of someone you love" } },
            { "Travel", new List<string> { "Take a photo of a landmark", "Take a photo of a street view", "Take a photo of a street sign", "Take a picture of your favorite vehicle" } }
        };

        foreach (var theme in themesWithDefault)
        {
            if (!_database.Table<Assignment>().Any(a => a.Theme == theme.Key))
            {
                // Add default assignments for this theme
                foreach (var assignmentName in theme.Value)
                {
                    _database.Insert(new Assignment
                    {
                        Theme = theme.Key,
                        Name = assignmentName,
                        PhotoPath = null
                    });
                }
            }
        }
    }
}

