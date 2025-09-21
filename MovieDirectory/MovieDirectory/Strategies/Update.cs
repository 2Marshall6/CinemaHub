using DataMovie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDirectory
{
    internal class Update : IActionMovie<UpdateMovieModel>
    {
        public void Action(Dictionary<int, Movie> movies, UpdateMovieModel data)
        {
            movies[data.Id].Description = data.Description;
        }
    }
}
