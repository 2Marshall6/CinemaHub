using DataMovie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MovieDirectory
{
    internal class Delete : IActionMovie<DeleteMovieModel>
    {
        public void Action(Dictionary<int, Movie> movies, DeleteMovieModel data)
        {
            var id = data.Id;

            movies.Remove(id);
        }
    }
}
