using DataAccounts.Entitys;
using DataAccounts.Entitys.MovieEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccounts.Repositories.MovieRepositories
{
    public interface IMovieRepository
    {
        Task CreateMovieAsync(Movie movie);
        Task UpdateMovieAsync(int id, string newDescription);
        Task DeleteMovieAsync(int id);
        Task<PageModel<Movie>> GetMoviesListAsync(GetListMovieModel filtrModel);
        Task<Movie> GetMovieByIdAsync(int movieId);
        Task AddGenresToMovieAsync(int movieId, List<int> genreIds);
    }
}
