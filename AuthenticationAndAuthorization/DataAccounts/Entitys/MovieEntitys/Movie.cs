using DataAccounts.Entitys.MovieEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccounts.Entitys
{
    public class Movie
    {
        public Movie(string name, string description)
        {
            Name = name;
            Description = description;
            MovieGenres = new List<MovieGenres>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Views { get; set; }
        public string? ExternalId { get; set; }
        public List<MovieGenres> MovieGenres { get; set; }
    }
}
