using AgileBoard.Api.Data;
using AgileBoard.Api.Logging;
using AgileBoard.Api.Repositories;
using AgileBoard.Api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AgileBoard.Api.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<DataContext>();

            builder.Services.AddScoped<IUserStoryService, UserStoryService>();
            builder.Services.AddScoped<IUserStoryRepository, UserStoryRepository>();
            builder.Services.AddTransient(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));
        }
    }
}
