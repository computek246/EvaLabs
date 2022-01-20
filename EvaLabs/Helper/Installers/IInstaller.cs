using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EvaLabs.Helper.Installers
{
    public interface IInstaller
    {
        void InstallServices(IServiceCollection services, IConfiguration configuration);

        int Order { get; }
    }
}
