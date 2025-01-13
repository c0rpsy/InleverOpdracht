using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crow.MVVM.Models
{
    public class Theme
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string IconPath { get; set; }
        [Ignore]
        public bool IsSelected { get; set; } // Track selection state
    }
}
