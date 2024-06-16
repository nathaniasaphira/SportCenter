using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportCenter.Services.Modals;
using SportCenter.Services.Navigators;
using SportCenter.Services.Users;
using SportCenter.ViewModels.Factories;

namespace SportCenter.HostBuilders;

public static class AddServicesHostBuilderExtensions
{
    public static IHostBuilder AddServices(this IHostBuilder host)
    {
        host.ConfigureServices(services =>
        {
            services.AddSingleton<IViewModelFactory, ViewModelFactory>();
            services.AddSingleton<IModalService, ModalService>();
            services.AddSingleton<INavigator, Navigator>();
            services.AddSingleton<ModalNavigator>();
            services.AddSingleton<IUserService, UserService>();
        });

        return host;
    }
}