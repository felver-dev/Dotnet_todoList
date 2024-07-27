using Microsoft.EntityFrameworkCore;
using todo_back.infrastructure.context;

namespace todo_back.infrastructure.extensions
{
	public static class AppContextServiceContext
	{
		public static IServiceCollection AddContextService(this IServiceCollection services, IConfiguration config)
		{
			services.AddDbContext<AppDbContext>(options =>
			{
				var DefaultConnection = config.GetConnectionString("DefaultConnection");
				options.UseMySql(DefaultConnection, ServerVersion.AutoDetect(DefaultConnection));
			});

			return services;
		}
	}
}