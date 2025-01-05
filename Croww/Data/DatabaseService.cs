using SQLite;
using Croww.Models;
using System.Linq;

public class DatabaseService
{
    private readonly SQLiteConnection _database;

    public DatabaseService(string dbPath)
    {
        _database = new SQLiteConnection(dbPath);
        _database.CreateTable<UserProfile>();
        _database.CreateTable<Assignment>();
    }

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
}

