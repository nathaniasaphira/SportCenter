using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportCenter.State.Modals;
using SportCenter.ViewModels;

namespace SportCenter.HostBuilders;

public static class AddViewModelsHostBuilderExtensions
{
    public static IHostBuilder AddViewModels(this IHostBuilder host)
    {
        host.ConfigureServices(services =>
        {
            services.AddSingleton<HomeViewModel>();
            services.AddSingleton<LoginModalViewModel>();
            services.AddSingleton<MainWindowViewModel>();

            services.AddSingleton<CreateViewModel<HomeViewModel>>(provider => provider.GetRequiredService<HomeViewModel>);
            services.AddSingleton<CreateViewModel<LoginModalViewModel>>(provider => () => CreateLoginModalViewModel(provider));
        });

        return host;
    }

    private static LoginModalViewModel CreateLoginModalViewModel(IServiceProvider provider)
    {
        return new LoginModalViewModel(provider.GetRequiredService<IModalService>());
    }
}