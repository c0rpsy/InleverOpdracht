using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crow.MVVM.Models.Services
{
    public class DatabaseService
    {
        private SQLiteConnection _database;

        public DatabaseService(string dbPath)
        {
            _database = new SQLiteConnection(dbPath);
            _database.CreateTable<Theme>();
            _database.CreateTable<Assignment>(); // Ensure the Assignment table is created
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
    }
}
