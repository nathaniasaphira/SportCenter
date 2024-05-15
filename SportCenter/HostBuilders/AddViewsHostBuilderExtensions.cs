using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportCenter.Services.Modals;
using SportCenter.Services.Navigators;
using SportCenter.ViewModels;
using SportCenter.Views;

namespace SportCenter.HostBuilders;

public static class AddViewsHostBuilderExtensions
{
    public static IHostBuilder AddViews(this IHostBuilder host)
    {
        host.ConfigureServices(services =>
        {
            services.AddSingleton(s => new MainWindow( 
                s.GetRequiredService<IModalService>(),
                s.GetRequiredService<ModalNavigator>())
            {
                DataContext = s.GetRequiredService<MainWindowViewModel>()
            });
        });

        return host;
    }
}