using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Croww.MVVM.Models
{
    public class UserProfile
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Username { get; set; }
        public string? Bio { get; set; }
        public string ProfileImagePath { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
