using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Model.MovieModel
{
    public record AddGenreModel(int movieId, List<int> genreIds);
}
