using AutoMapper;
using BusinessLogic.Interfaces;
using BusinessLogic.Model.GenreModel;
using BusinessLogic.Model.MovieModel;
using DataAccounts.Entitys;
using DataAccounts.Entitys.MovieEntitys;
using DataAccounts.Repositories.GenreRopository;
using DataAccounts.Repositories.MovieRepositories;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BusinessLogic.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public MovieService(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }
        public async Task CreateMovieAsync(CreateMovieModel createModel)
        {
            var movie = new Movie(createModel.name, createModel.description)
            {
                Id = createModel.id,
                MovieGenres = createModel.genresIds.Select(id => new MovieGenres { GenreId = id }).ToList()
            };

            await _movieRepository.CreateMovieAsync(movie);
        }
            
        public async Task UpdateMovieAsync(UpdateMovieModel updateModel)
        {
            await _movieRepository.UpdateMovieAsync(updateModel.id,updateModel.newDescription);
        }

        public async Task DeleteMovieAsync(DeleteMovieModel deleteModel)
        {
            await _movieRepository.DeleteMovieAsync(deleteModel.id);
        }

        public async Task<PageModel<MovieInfo>> MovieFilter(GetListMovieModel filtrModel)
        {
            var moviePage =  await _movieRepository.GetMoviesListAsync(filtrModel);
            var movies = _mapper.Map<List<MovieInfo>>(moviePage.Items);

            return new PageModel<MovieInfo>(
                currentPage: moviePage.CurrentPage,
                nextPage: moviePage.NextPage,
                totalCount: moviePage.TotalCount,
                items: movies
            );
        }

        public async Task<MovieInfo> GetMovieAsync(GetMovieModel getModel)
        {
            var movie = await _movieRepository.GetMovieByIdAsync(getModel.movieId);

            return _mapper.Map<MovieInfo>(movie);
        }

        public async Task AddGenresAsync(AddGenreModel addGenreModel)
        {
            await _movieRepository.AddGenresToMovieAsync(addGenreModel.movieId, addGenreModel.genreIds);
        }
    }
}
