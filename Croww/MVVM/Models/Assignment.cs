using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Croww.MVVM.Models
{
    public class Assignment
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Theme { get; set; }
        public string Name { get; set; }
        public string PhotoPath { get; set; } // Path to the saved photo
    }
}
