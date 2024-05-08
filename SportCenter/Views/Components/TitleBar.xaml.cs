using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SportCenter.Utils;

namespace SportCenter.Views.Components;

public partial class TitleBar : UserControl
{
    public static event RoutedEventHandler? MaximizeClicked = delegate { };
    public static event RoutedEventHandler? MinimizeClicked = delegate { };
    public static event RoutedEventHandler? CloseClicked = delegate { };
    public static event MouseEventHandler? TitleBarDragged = delegate { };

    private const string DefaultCornerRadiusResource = "TitleBarCornerRadius";
    private Point? _initialMousePosition;

    public TitleBar()
    {
        InitializeComponent();

        MainWindow.MaximizeFired += Window_MaximizeFired;

        ToggleMaximizeVisibility(WindowStateManager.IsMaximized);
    }

    private void Dispose(bool disposing)
    {
        if (!disposing)
        {
            return;
        }

        MainWindow.MaximizeFired -= Window_MaximizeFired;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~TitleBar()
    {
        Dispose(false);
    }

    #region Event Handlers

    private void Window_MaximizeFired(object? sender, bool isMaximized)
    {
        SwitchTitleBarCornerState(isMaximized);
        ToggleMaximizeVisibility(isMaximized);
    }

    private void SwitchTitleBarCornerState(bool maximized)
    {
        TitleBarBorder.CornerRadius = maximized 
            ? new CornerRadius(0) 
            : (CornerRadius)FindResource(DefaultCornerRadiusResource);
    }

    private void ToggleMaximizeVisibility(bool maximized)
    {
        BtnRestore.Visibility = maximized 
            ? Visibility.Visible : Visibility.Collapsed;

        BtnMaximize.Visibility = maximized 
            ? Visibility.Collapsed : Visibility.Visible;
    }

    #endregion Event Handlers

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