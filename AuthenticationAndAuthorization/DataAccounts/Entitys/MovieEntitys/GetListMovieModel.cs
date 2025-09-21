using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccounts.Entitys
{
    public record GetListMovieModel(string? name, int? minViews, string[]? genres, int pageNumber = 1, int pageSize = 10);
}
