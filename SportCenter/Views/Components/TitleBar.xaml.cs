using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SportCenter.Utils;

namespace SportCenter.Views.Components;

public partial class TitleBar : UserControl
{
    public static event RoutedEventHandler? MaximizeClicked;
    public static event RoutedEventHandler? MinimizeClicked;
    public static event RoutedEventHandler? CloseClicked;
    public static event MouseEventHandler? TitleBarDragged;

    private Point? _initialMousePosition;

    public TitleBar()
    {
        InitializeComponent();

        MainWindow.MaximizeFired += (_, _) => ToggleMaximizeVisibility(WindowStateManager.IsMaximized);

        BtnRestore.Visibility = Visibility.Collapsed;
    }

    private void ToggleMaximizeVisibility(bool isMaximized)
    {
        BtnRestore.Visibility = isMaximized ? Visibility.Visible : Visibility.Collapsed;
        BtnMaximize.Visibility = isMaximized ? Visibility.Collapsed : Visibility.Visible;
    }

    #region Button Callbacks

    private void BtnMaximize_Click(object sender, RoutedEventArgs e) => MaximizeClicked?.Invoke(this, e);

    private void BtnMinimize_Click(object sender, RoutedEventArgs e) => MinimizeClicked?.Invoke(this, e);

    private void BtnClose_Click(object sender, RoutedEventArgs e) => CloseClicked?.Invoke(this, e);

    #endregion

    #region TitleBar Drag

    private void TitleBar_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton is not MouseButtonState.Pressed)
        {
            return;
        }

        _initialMousePosition = e.GetPosition(this);

        if (e.ClickCount == 2)
        {
            MaximizeClicked?.Invoke(this, e);
        }
    }

    private void TitleBar_OnMouseMove(object sender, MouseEventArgs e)
    {
        if (_initialMousePosition is null || e.LeftButton is not MouseButtonState.Pressed)
        {
            _initialMousePosition = null;
            return;
        }

        var currentMousePosition = e.GetPosition(this);

        if (currentMousePosition == _initialMousePosition)
        {
            return;
        }

        TitleBarDragged?.Invoke(this, e);
        _initialMousePosition = null;
    }

    #endregion
}