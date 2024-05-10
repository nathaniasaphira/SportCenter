﻿using Microsoft.Extensions.DependencyInjection;
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
            services.AddViewModel<MainWindowViewModel>();
            services.AddViewModel<HomeViewModel>();
            services.AddViewModel<LoginModalViewModel>();
            services.AddViewModel<LoadingModalViewModel>();
        });

        return host;
    }
}

public static class ServiceCollectionExtensions
{
    public static void AddViewModel<TViewModel>(this IServiceCollection services) where TViewModel : ViewModelBase
    {
        services.AddSingleton<TViewModel>();
        services.AddSingleton<CreateViewModel<TViewModel>>(provider => provider.GetRequiredService<TViewModel>);
    }
}