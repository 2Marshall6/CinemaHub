using AuthenticationAndAuthorization.Policy;
using BusinessLogic.Interfaces;
using BusinessLogic.Model.MovieModel;
using DataAccounts.Entitys;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAndAuthorization.Endpoints
{
    public static class MovieEndpoints
    {
        public static void MapMovieEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("movies", ([FromServices] IMovieService movieService, [FromBody] CreateMovieModel createModel)
                => movieService.CreateMovieAsync(createModel)).RequireAuthorization(Policies.AdminPolicy);

            endpoints.MapPut("movies", ([FromServices] IMovieService movieService, [FromBody] UpdateMovieModel updateModel)
                => movieService.UpdateMovieAsync(updateModel)).RequireAuthorization(Policies.AdminPolicy);

            endpoints.MapDelete("movies", ([FromServices] IMovieService movieService, [FromBody] DeleteMovieModel deleteModel)
                => movieService.DeleteMovieAsync(deleteModel)).RequireAuthorization(Policies.AdminPolicy);

            endpoints.MapPost("movies/{movieId}/genres/{genreId}", ([FromServices] IMovieService movieService, [FromBody] AddGenreModel addGenreModel)
                => movieService.AddGenresAsync(addGenreModel)).RequireAuthorization(Policies.AdminPolicy);

            endpoints.MapGet("movies/{filterMovie}", ([FromServices] IMovieService movieService, [AsParameters] GetListMovieModel filterModel)
               => movieService.MovieFilter(filterModel)).RequireAuthorization(Policies.UserPolicy);

            endpoints.MapGet("movies/{movieId:int}", ([FromServices] IMovieService movieService, [AsParameters] GetMovieModel getModel)
                => movieService.GetMovieAsync(getModel)).RequireAuthorization(Policies.UserPolicy);
        }
    }
}
