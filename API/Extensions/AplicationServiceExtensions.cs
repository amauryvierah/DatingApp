using API.Data;
using API.Interface;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class AplicationServiceExtensions
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection services, 
            IConfiguration config)
        {            
            //Get connection string from start
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });
            services.AddCors(); //Add cors to allow calls from angular client            
            services.AddScoped<ITokenService, TokenService>();//Token Dependency Injection
            return services;
        }
    }
}
