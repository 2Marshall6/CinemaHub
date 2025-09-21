using BusinessLogic.Model.MovieModel;
using DataAccounts.Entitys;

namespace BusinessLogic.Interfaces
{
    public interface IMovieService
    {
        Task CreateMovieAsync(CreateMovieModel createModel);
        Task UpdateMovieAsync(UpdateMovieModel updateModel);
        Task DeleteMovieAsync(DeleteMovieModel deleteModel);
        Task<MovieInfo> GetMovieAsync(GetMovieModel getModel);
        Task<PageModel<MovieInfo>> MovieFilter(GetListMovieModel filtrModel);
        Task AddGenresAsync(AddGenreModel addGenreModel);
    }
}
