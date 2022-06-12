﻿namespace AgileBoard.Api.Installers
{
    public static class InstallerExtensions
    {
        public static void InstallServicesInAssembly(this WebApplicationBuilder builder)
        {
            var installers = typeof(Program).Assembly.ExportedTypes.Where(x =>
           typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).Select(Activator.CreateInstance).Cast<IInstaller>().ToList();

            installers.ForEach(Installer => Installer.InstallServices(builder));
        }
    }
}
