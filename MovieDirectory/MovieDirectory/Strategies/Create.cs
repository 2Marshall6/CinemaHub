using DataMovie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDirectory
{
    internal class Create : IActionMovie<CreateMovieModel>
    {
        public void Action(Dictionary<int, Movie> movies, CreateMovieModel data)
        {
            movies.Add(data.Id,new Movie(data.Name, data.Description));
        }
    }
}
