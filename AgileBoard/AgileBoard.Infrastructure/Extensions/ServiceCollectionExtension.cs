using AgileBoard.Infrastructure.Installers;
using Microsoft.AspNetCore.Builder;

namespace AgileBoard.Infrastructure.Extensions
{

    public static class ServiceCollectionExtension
    {
        private static DbInstaller _installer = new();

        public static void AddInfrastructre(this WebApplicationBuilder builder)
        {
            _installer.InstallServices(builder);
        }
    }
}
