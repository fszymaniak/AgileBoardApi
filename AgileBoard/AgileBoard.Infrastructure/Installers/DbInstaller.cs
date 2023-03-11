using AgileBoard.Core.Abstractions;
using AgileBoard.Core.Installers;
using AgileBoard.Core.Logging;
using AgileBoard.Core.Repositories;
using AgileBoard.Core.Services;
using AgileBoard.Infrastructure.DAL;
using AgileBoard.Infrastructure.Repositories;
using AgileBoard.Infrastructure.Time;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AgileBoard.Infrastructure.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AgileBoardDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AgileBoardDbContext>();

            builder.Services.AddTransient<IClock, Clock>();

            builder.Services.AddScoped<IUserStoryService, UserStoryService>();
            builder.Services.AddScoped<IUserStoryRepository, UserStoryRepository>();
            builder.Services.AddTransient(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));
        }
    }
}
