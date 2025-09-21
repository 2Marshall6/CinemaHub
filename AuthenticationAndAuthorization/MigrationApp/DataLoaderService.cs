using DataAccounts;
using DataAccounts.Entitys.MovieEntitys;
using DataAccounts.Entitys;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using DataAccounts.Repositories;
using System.Data;
using System.Threading.Channels;

namespace DataLoader
{
    public class DataLoaderService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;

        public DataLoaderService(ApplicationDbContext context, IUserRepository userRepositories)
        {
            _context = context;
            _userRepository = userRepositories;
        }

        public async Task LoadDataAsync()
        {
            Console.WriteLine("File path:");
            var jsonData = File.ReadAllText(Console.ReadLine());
            var rootJson = JsonConvert.DeserializeObject<RootJson>(jsonData);

            await LoadUsersAsync(rootJson.Users);
            await LoadGenresAsync(rootJson.Genres);
            await LoadMoviesAsync(rootJson.Movies);
        }

        public async Task LoadUsersAsync(List<User> users)
        {
            foreach (var userJson in users)
            {
                try
                {
                    string role = UserConstants.User;
                    string defaultPassword = "DefaultPassword123";

                    var user = new User
                    {
                        UserName = userJson.UserName,
                        Email = userJson.Email,
                        ExternalId = userJson.Id.ToString(),
                    };

                    if (user.Email.Equals("admin@example.com", StringComparison.OrdinalIgnoreCase))
                    {
                        role = UserConstants.Admin;
                    }

                    await _userRepository.Register(user, defaultPassword, role);
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    throw new InvalidOperationException("Failed to get the users into the system");
                }
            }
        }

        public async Task LoadGenresAsync(List<Genre> genres)
        {
            foreach (var genreJson in genres)
            {
                try
                {
                    var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Name == genreJson.Name);
                    if (genre == null)
                    {
                        genre = new Genre(genreJson.Name) { ExternalId = genreJson.Id.ToString() };
                        _context.Genres.Add(genre);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (Exception)
                {
                    throw new InvalidOperationException("failed to get the genres into the system");
                }
            }
        }

        public async Task LoadMoviesAsync(List<Movie> movies)
        {
            foreach (var movieJson in movies)
            {
                try
                {
                    var movie = await _context.Movies.FirstOrDefaultAsync(g => g.Name == movieJson.Name);

                    movie = new Movie(movieJson.Name, movieJson.Description)
                    {
                        Views = movieJson.Views,
                        ExternalId = movieJson.Id.ToString(),
                        MovieGenres = movieJson.MovieGenres.Select(genre => new MovieGenres { GenreId = genre.GenreId }).ToList()
                    };

                    _context.Movies.Add(movie);
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    throw new InvalidOperationException("failed to get the movies into the system");
                }
            }
        }
    }
}
