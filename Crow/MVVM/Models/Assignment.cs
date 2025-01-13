using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Crow.MVVM.Models
{
    public class Assignment
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PhotoPath { get; set; } // Path to the saved photo
        public ObservableCollection<string> Comments { get; set; }
        public int Likes { get; set; }
        public string NewComment { get; set; }
    }
}
