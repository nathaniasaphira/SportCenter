using System.Windows;
using HandyControl.Data;
using HandyControl.Themes;
using HandyControl.Tools;

namespace SportCenter;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

#if DEBUG
        UpdateSkin(SkinType.Default);
#endif
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