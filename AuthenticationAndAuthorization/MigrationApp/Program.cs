using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DataAccounts;
using DataLoader;
using Microsoft.EntityFrameworkCore;
using DataAccounts.Repositories;
using Microsoft.AspNetCore.Identity;
using AuthenticationAndAuthorization.Extensions;

class Program
{
    static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        var migrationService = host.Services.GetRequiredService<DataLoaderService>();


        await migrationService.LoadDataAsync();
    }

    static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                ServiceExtensions.ConfigureServices(services, context.Configuration);
                services.AddTransient<DataLoaderService>();
            });
}
