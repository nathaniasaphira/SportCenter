using HandyControl.Data;
using HandyControl.Themes;
using HandyControl.Tools;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportCenter.HostBuilders;
using SportCenter.Views;
using System.Reflection;
using System.Windows;

namespace SportCenter;

public partial class App : Application
{
    public static IHost? AppHost { get; private set; }

    private static Mutex? _mutex = null;

    public App()
    {
        if (IsAlreadyRunning())
        {
            Current.Shutdown();
            return;
        }

        AppHost = Host.CreateDefaultBuilder()
            .AddViewModels()
            .AddViews()
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost!.StartAsync();

        Window window = AppHost.Services.GetRequiredService<MainWindow>();
        window.Show();

        base.OnStartup(e);

#if DEBUG
        UpdateSkin(SkinType.Default);
#endif
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost!.StopAsync();
        AppHost.Dispose();

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

    private static bool IsAlreadyRunning()
    {
        string appName = ((AssemblyTitleAttribute)Assembly.GetExecutingAssembly()
            .GetCustomAttributes(typeof(AssemblyTitleAttribute), false)[0]).Title;

        _mutex = new Mutex(true, appName, out bool createdNew);

        return !createdNew;
    }
}