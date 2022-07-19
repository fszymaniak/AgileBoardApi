using AgileBoard.Api.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AgileBoard.Api.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<UserStory> UserStories { get; set; }
    }
}
