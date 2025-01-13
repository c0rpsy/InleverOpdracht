using System.Threading.Tasks;
using Crow.MVVM.Models;

namespace Crow.Interfaces
{
    // Repository Interface for Dependency Injection
    public interface IUserRepository
    {
        // Register a user asynchronously; returns false if user already exists.
        Task<bool> RegisterUserAsync(UserProfile user);

        // Returns the *first* user found in the DB, or null if none exist.
        Task<UserProfile> GetUserProfileAsync();

        // Inserts or updates a user profile in the DB.
        Task SaveUserProfileAsync(UserProfile user);
    }
} 
