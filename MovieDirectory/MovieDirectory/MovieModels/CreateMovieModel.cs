using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDirectory
{
    public class CreateMovieModel
    {
        public CreateMovieModel(int id,string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
        public int Id { get; set; } 
        public string Name { get; set; } 
        public string Description { get; set; }
    }
}
