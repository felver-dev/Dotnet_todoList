using Microsoft.EntityFrameworkCore;
using todo_back.infrastructure.models;

namespace todo_back.infrastructure.context
{
	public class AppDbContext : DbContext
	{
	   public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
	   
	   public DbSet<TaskModel> Tasks { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
		}
	}
}