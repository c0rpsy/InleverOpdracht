using System.Threading.Tasks;
using SQLite;
using Crow.Interfaces;
using Crow.MVVM.Models;

namespace Crow.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SQLiteAsyncConnection _database;

        public UserRepository(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<UserProfile>().Wait();
        }

        // Register a new user if no existing user has the same username or email.
        public async Task<bool> RegisterUserAsync(UserProfile user)
        {
            var existingUser = await _database.Table<UserProfile>()
                .Where(u => u.Username == user.Username || u.Email == user.Email)
                .FirstOrDefaultAsync();

            if (existingUser != null)
            {
                // User already exists
                return false;
            }

            await _database.InsertAsync(user);
            return true;
        }

        // Returns the first user in the table (null if none exist).
        public async Task<UserProfile> GetUserProfileAsync()
        {
            return await _database.Table<UserProfile>()
                                  .FirstOrDefaultAsync();
        }

        // Inserts or updates the provided user profile.
        public async Task SaveUserProfileAsync(UserProfile user)
        {
            var existingUser = await _database.Table<UserProfile>()
                                              .Where(u => u.Id == user.Id)
                                              .FirstOrDefaultAsync();
            if (existingUser != null)
            {
                await _database.UpdateAsync(user);
            }
            else
            {
                await _database.InsertAsync(user);
            }
        }
    }
}
