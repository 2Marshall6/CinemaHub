using DataAccounts.Entitys.MovieEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Model.MovieModel
{
    public record CreateMovieModel(int id, string name, string description, List<int> genresIds);
}
