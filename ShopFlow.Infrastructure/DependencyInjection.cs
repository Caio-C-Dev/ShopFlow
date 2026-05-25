using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopFlow.Application.Identity;
using ShopFlow.Domain.Interfaces;
using ShopFlow.Infrastructure.Identity;
using ShopFlow.Infrastructure.Persistence;
using ShopFlow.Infrastructure.Persistence.Repositories;

namespace ShopFlow.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));

        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 8;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
        })
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();

        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<JwtService>();
        
        
        return services;


    }
}