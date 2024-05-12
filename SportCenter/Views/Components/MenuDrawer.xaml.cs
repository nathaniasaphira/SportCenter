using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SportCenter.Utils;

namespace SportCenter.Views.Components;

public partial class MenuDrawer : UserControl
{
    public static readonly DependencyProperty IsMenuDrawerOpenProperty =
        DependencyProperty.Register(nameof(IsMenuDrawerOpen), typeof(bool), typeof(MenuDrawer),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsMenuDrawerOpenChanged));

    public bool IsMenuDrawerOpen
    {
        get => (bool)GetValue(IsMenuDrawerOpenProperty);
        set => SetValue(IsMenuDrawerOpenProperty, value);
    }

    private static void OnIsMenuDrawerOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var newValue = (bool)e.NewValue;

        if (d is not MenuDrawer menuDrawer)
        {
            return;
        }

        if (newValue)
        {
            menuDrawer!.OpenMenuDrawer();
        }
        else
        {
            menuDrawer!.CloseMenuDrawer();
        }
    }

    public MenuDrawer()
    {
        InitializeComponent();

        IsMenuDrawerOpen = false;
        InputBlocker.Visibility = Visibility.Collapsed;
    }

    private void OpenMenuDrawer()
    {
        AnimationManager.AnimateSlideHorizontal(DrawerBorder, -450, 0,
            TimeSpan.FromSeconds(0.2));

        InputBlocker.Visibility = Visibility.Visible;
        AnimationManager.AnimateExpandFadeIn(InputBlocker);
    }

    private void CloseMenuDrawer()
    {
        AnimationManager.AnimateSlideHorizontal(DrawerBorder, 0, -450,
            TimeSpan.FromSeconds(0.2));

        InputBlocker.Visibility = Visibility.Collapsed;
        AnimationManager.AnimateShrinkFadeOut(InputBlocker);
    }

    private void InputBlocker_OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        IsMenuDrawerOpen = false;
    }
}