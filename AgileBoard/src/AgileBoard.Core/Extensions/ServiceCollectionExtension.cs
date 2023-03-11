using AgileBoard.Core.Abstractions;
using AgileBoard.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AgileBoard.Core.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddScoped<IUserStoryService, UserStoryService>();
            

            return services;
        }
    }
}
