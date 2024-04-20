using E_Commerece.API.ExceptionsConfiguration;
using InfraStructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_Commerece.API.Extensions.IdentityExtensions
{
    public static class IdentityServiceExtension
    {
        public static void AddIdentityService(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddDbContext<AppIdentityDbContext>
            (options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));

            });

        }
    }
}
