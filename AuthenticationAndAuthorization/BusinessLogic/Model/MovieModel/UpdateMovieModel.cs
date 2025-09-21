using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Model.MovieModel
{
    public record UpdateMovieModel(int id, string newDescription);
}
