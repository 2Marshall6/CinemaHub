using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using BusinessLogic;
using DataAccounts.Repositories.GenreRepositories;
using DataAccounts.Repositories.GenreRopository;
using DataAccounts.Repositories.MovieRepositories;
using DataAccounts.Repositories;
using DataAccounts;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationAndAuthorization.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApiAuthencation(configuration);
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddControllers();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<RoleProvider>();
            services.AddHttpContextAccessor();

            services.AddDbContextPool<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DataBase")));

            services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
        }
    }
}
