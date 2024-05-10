using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportCenter.State.Modals;
using SportCenter.ViewModels;
using SportCenter.Views;

namespace SportCenter.HostBuilders;

public static class AddViewsHostBuilderExtensions
{
    public static IHostBuilder AddViews(this IHostBuilder host)
    {
        host.ConfigureServices(services =>
        {
            services.AddSingleton(s => new MainWindow(s.GetRequiredService<MainWindowViewModel>(), 
                s.GetRequiredService<IModalService>()));
        });

        return host;
    }
}