using API.Application.Repositories;
using API.Application.Services;
using API.Infrastructure.Persistance;
using API.Infrastructure.Repositories;
using API.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));

        services.AddScoped<ICategoryRepository, CategoryRepository>();

        services.AddScoped<ICategoryService, CategoryManager>();

        return services;
    }
}
