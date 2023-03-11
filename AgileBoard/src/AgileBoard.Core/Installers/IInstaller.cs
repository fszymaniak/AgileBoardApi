using Microsoft.AspNetCore.Builder;

namespace AgileBoard.Core.Installers
{
    public interface IInstaller
    {
        void InstallServices(WebApplicationBuilder builder);
    }
}
