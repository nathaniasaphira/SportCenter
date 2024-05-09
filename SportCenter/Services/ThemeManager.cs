using System.Windows;
using HandyControl.Data;
using HandyControl.Tools;

namespace SportCenter.Services;

public static class ThemeManager
{
    public static SkinType CurrentSkinType { get; set; }

    public static void UpdateSkin(SkinType skin)
    {
        Application.Current.Resources.MergedDictionaries.Add(ResourceHelper.GetSkin(skin));
        Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary
        {
            Source = new Uri("pack://application:,,,/HandyControl;component/Themes/Theme.xaml")
        });
        Application.Current.MainWindow?.OnApplyTemplate();

        CurrentSkinType = skin;
    }
}