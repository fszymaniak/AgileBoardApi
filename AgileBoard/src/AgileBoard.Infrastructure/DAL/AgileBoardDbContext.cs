using AgileBoard.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace AgileBoard.Infrastructure.DAL
{
    public sealed class AgileBoardDbContext : DbContext
    {
        public DbSet<UserStory> UserStories { get; set; }

        public AgileBoardDbContext(DbContextOptions<AgileBoardDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }


    }
}
