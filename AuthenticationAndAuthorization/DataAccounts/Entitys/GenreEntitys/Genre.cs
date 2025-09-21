using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccounts.Entitys.MovieEntitys
{
    public class Genre
    {
        public Genre(string name)
        {
            Name = name;
            MovieGenres = new List<MovieGenres>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string? ExternalId { get; set; }
        public List<MovieGenres> MovieGenres { get; set; }
    }
}
