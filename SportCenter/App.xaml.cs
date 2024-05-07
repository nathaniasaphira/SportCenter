using HandyControl.Data;
using HandyControl.Themes;
using HandyControl.Tools;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportCenter.Views;
using System.Windows;

namespace SportCenter;

public partial class App : Application
{
    public static IHost? AppHost { get; private set; }

    public App()
    {
        AppHost = Host.CreateDefaultBuilder()
                       .ConfigureServices((hostContext, services) =>
                       {
                           services.AddSingleton<MainWindow>();
                       })
                       .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost!.StartAsync();

        MainWindow appWindow = AppHost.Services.GetRequiredService<MainWindow>();
        appWindow.Show();

        base.OnStartup(e);

#if DEBUG
        UpdateSkin(SkinType.Dark);
#endif
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost!.StopAsync();
        base.OnExit(e);
    }

    public void UpdateSkin(SkinType skin)
    {
        SharedResourceDictionary.SharedDictionaries.Clear();
        Resources.MergedDictionaries.Add(ResourceHelper.GetSkin(skin));
        Resources.MergedDictionaries.Add(new ResourceDictionary
        {
            Source = new Uri("pack://application:,,,/HandyControl;component/Themes/Theme.xaml")
        });
        Current.MainWindow?.OnApplyTemplate();
    }
}