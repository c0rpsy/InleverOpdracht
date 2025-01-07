using Crow.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crow.Interfaces
{
    // Repository Interface for Dependency Injection
    public interface IUserRepository
    {
        Task<bool> RegisterUserAsync(UserProfile user);
        Task<bool> RegisterUser(UserProfile user);

    }
}
