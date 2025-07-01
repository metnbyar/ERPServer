using ERPServer.Domain.Entities;
using ERPServer.Infrastructure.Context;
using ERPServer.Infrastructure.Options;
using ERPServer.Domain.Repositories; // IProductRepository için eklenmeli
using ERPServer.Infrastructure.Repositories; // ProductRepository için eklenmeli
using GenericRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using ERPServer.Application.Services;
using ERPServer.Infrastructure.Services;

namespace ERPServer.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // DbContext ayarı
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseMySql(configuration.GetConnectionString("MySql"),
                                  new MySqlServerVersion(new Version(8, 0, 21))); // MySQL versiyonunu belirtmeniz gerekebilir
            });


            // UnitOfWork kaydı
            services.AddScoped<IUnitOfWork>(srv => srv.GetRequiredService<ApplicationDbContext>());

            // Identity ayarları
            services
                .AddIdentity<AppUser, IdentityRole<Guid>>(cfr =>
                {
                    cfr.Password.RequiredLength = 1;
                    cfr.Password.RequireNonAlphanumeric = false;
                    cfr.Password.RequireUppercase = false;
                    cfr.Password.RequireLowercase = false;
                    cfr.Password.RequireDigit = false;
                    cfr.SignIn.RequireConfirmedEmail = true;
                    cfr.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    cfr.Lockout.MaxFailedAccessAttempts = 3;
                    cfr.Lockout.AllowedForNewUsers = true;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // JWT Ayarları
            services.Configure<JwtOptions>(configuration.GetSection("Jwt"));
            services.ConfigureOptions<JwtTokenOptionsSetup>();
            services.AddAuthentication()
                .AddJwtBearer();
            services.AddAuthorizationBuilder();

            // Scrutor kullanımı devre dışı
            // services.Scan(...) kısmını kaldırıyoruz.

            // Manuel olarak Dependency Injection ayarları
            services.AddScoped<IJwtProvider, JwtProvider>();
            services.AddScoped<IDepotRepository, DepotRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<IRecipeDetailRepository,RecipeDetailRepository>();
            services.AddScoped<IOrderRepository,OrderRepository>();
            services.AddScoped<IOrderDetailRepository,OrderDetailRepository>();

            // Ek diğer repository sınıflarını ekle
            // Örnek: services.AddScoped<IAnotherRepository, AnotherRepository>();

            return services;
        }
    }
}
