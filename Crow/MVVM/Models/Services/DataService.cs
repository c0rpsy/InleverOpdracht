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
    }
}
