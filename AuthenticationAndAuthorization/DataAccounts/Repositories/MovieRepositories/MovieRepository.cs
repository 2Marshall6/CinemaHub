using DataAccounts.Entitys;
using DataAccounts.Entitys.MovieEntitys;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DataAccounts.Repositories.MovieRepositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext _context;

        public MovieRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateMovieAsync(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMovieAsync(int id, string newDescription)
        {
            await _context.Movies
                .Where(c => c.Id == id)
                .ExecuteUpdateAsync(s =>
                s.SetProperty(m => m.Description, newDescription));
        }

        public async Task DeleteMovieAsync(int id)
        {
            await _context.Movies
                .Where(c => c.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<Movie?> GetMovieByIdAsync(int movieId)
        {
            var movie = await _context.Movies
                .Include(m => m.MovieGenres)
                .ThenInclude(mg => mg.Genre)
                .FirstOrDefaultAsync(m => m.Id == movieId);

            return movie;
        }

        public async Task<PageModel<Movie>> GetMoviesListAsync(GetListMovieModel filterModel)
        {
            var query = _context.Movies.AsQueryable();

            if (!string.IsNullOrEmpty(filterModel.name))
            {
                query = query.Where(m => m.Name.Contains(filterModel.name));
            }

            if (filterModel.minViews != null)
            {
                query = query.Where(m => m.Views >= filterModel.minViews);
            }

            if (filterModel.genres != null && filterModel.genres.Any())
            {
                query = query.Where(m => m.MovieGenres.Any(mg => filterModel.genres.Contains(mg.Genre.Name)));
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((filterModel.pageNumber - 1) * filterModel.pageSize)
                .Take(filterModel.pageSize)
                .Include(m => m.MovieGenres)
                .ThenInclude(mg => mg.Genre)
                .ToListAsync();

            var nextPage = (filterModel.pageNumber * filterModel.pageSize) < totalCount ? (int?)filterModel.pageNumber + 1 : null;

            return new PageModel<Movie>(
                currentPage: filterModel.pageNumber,
                nextPage: nextPage,
                totalCount: totalCount,
                items: items
            );
        }

        public async Task AddGenresToMovieAsync(int movieId, List<int> genreIds)
        {
            var movie = await _context.Movies
                .Include(m => m.MovieGenres)
                .FirstOrDefaultAsync(m => m.Id == movieId);

            foreach (var genreId in genreIds)
            {
                if (!movie.MovieGenres.Any(mg => mg.GenreId == genreId))
                {
                    movie.MovieGenres.Add(new MovieGenres { MovieId = movieId, GenreId = genreId });
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
