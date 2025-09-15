using AuthenticationAndAuthorization;
using AuthenticationAndAuthorization.Endpoints;
using AuthenticationAndAuthorization.Extensions;
using DataAccounts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.ApplyMigrations();

using (var scope = app.Services.CreateScope())
{
    var roleProvider = scope.ServiceProvider.GetRequiredService<RoleProvider>();
    await roleProvider.AddRoles();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapUserEndpoints();
app.MapMovieEndpoints();
app.MapGenreEndpoints();

app.Run();

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using ApplicationDbContext dbContext =
            scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        dbContext.Database.Migrate();
    }
}