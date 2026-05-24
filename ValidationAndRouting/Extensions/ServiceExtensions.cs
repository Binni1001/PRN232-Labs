using Microsoft.EntityFrameworkCore;
using ValidationAndRouting.Context;

namespace ValidationAndRouting.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSqlContext( this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RepositoryContext>(opts =>
                opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));
        }
    }
}
