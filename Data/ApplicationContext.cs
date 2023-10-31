using Microsoft.EntityFrameworkCore;

namespace WebApplication10.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Models.User> Users { get; set; } = null!;
        public DbSet<Models.AuthorizeUser> AuthorizeUsers { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                modelBuilder.Entity<Models.User>().HasData(new Models.User {Id = 1, Name = "Tom", Age = 23 },
                new Models.User {Id = 2, Name = "Sam", Age = 10 },
                new Models.User { Id = 3, Name = "Jake", Age = 30});
        }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
            optionsBuilder.LogTo(Console.WriteLine);
		}
	}
}
