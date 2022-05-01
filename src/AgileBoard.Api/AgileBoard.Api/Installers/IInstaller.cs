using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweetAppDotNet5
{
    public interface IInstaller
    {
        void InstallServices(WebApplicationBuilder builder);
    }
}
