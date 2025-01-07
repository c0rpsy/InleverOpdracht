using Crow.MVVM.Models;
using SQLite;
using Crow.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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

        public Task<bool> RegisterUser(UserProfile user)
        {
            return RegisterUserAsync(user);
        }
    }



}
