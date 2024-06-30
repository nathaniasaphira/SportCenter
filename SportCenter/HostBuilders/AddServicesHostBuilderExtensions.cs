using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportCenter.Repositories;
using SportCenter.Services.Auth;
using SportCenter.Services.Modals;
using SportCenter.Services.Navigators;
using SportCenter.Models.Entities;
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
            services.AddSingleton<IRepository<User>, UserRepository>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<ModalNavigator>();
        });

        return host;
    }
}