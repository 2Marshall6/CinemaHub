using AuthenticationAndAuthorization.Policy;
using BusinessLogic.Interfaces;
using BusinessLogic.Model.GenreModel;
using DataAccounts.Entitys.GenreEntitys;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAndAuthorization.Endpoints
{
    public static class GenreEndpoints
    {
        public static void MapGenreEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("genres", ([FromServices] IGenreService genreService, [FromBody] CreateGenreModel genre)
                => genreService.CreateGenre(genre)).RequireAuthorization(Policies.AdminPolicy);

            endpoints.MapDelete("genres/{id:int}", ([FromServices] IGenreService genreService, [FromBody] DeleteGenreModel genre)
                => genreService.DeleteGenre(genre)).RequireAuthorization(Policies.AdminPolicy);

            endpoints.MapGet("genres", ([FromServices] IGenreService genreService, [AsParameters] GetGenresListModel filterModel)
                => genreService.GetGenre(filterModel)).RequireAuthorization(Policies.UserPolicy);
        }
    }
}
