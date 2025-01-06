using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Crow.MVVM.Models
{
    public class Assignments
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Theme { get; set; }
        public string Name { get; set; }
        public string PhotoPath { get; set; } // Path to the saved photo
    }
}
