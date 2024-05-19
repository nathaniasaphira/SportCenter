using HandyControl.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportCenter.HostBuilders;
using SportCenter.Views;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using SportCenter.Services;

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
            .AddDatabase()
            .AddServices()
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
        InputBinding toggleTheme = new KeyBinding(new RelayCommand(() =>
        {
            ThemeManager.UpdateSkin(ThemeManager.CurrentSkinType is SkinType.Default ? SkinType.Dark : SkinType.Default);
        }), Key.F12, ModifierKeys.None);
        window.InputBindings.Add(toggleTheme);
#endif
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost!.StopAsync();
        AppHost.Dispose();

        base.OnExit(e);
    }

    private static bool IsAlreadyRunning()
    {
        string appName = ((AssemblyTitleAttribute)Assembly.GetExecutingAssembly()
            .GetCustomAttributes(typeof(AssemblyTitleAttribute), false)[0]).Title;

        _mutex = new Mutex(true, appName, out bool createdNew);

        return !createdNew;
    }
}