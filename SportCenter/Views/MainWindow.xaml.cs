using System.Windows;
using System.Windows.Input;
using SportCenter.Views.Components;

namespace SportCenter.Views;

public partial class MainWindow : Window
{
    private bool _isResizing = false;

    public MainWindow()
    {
        TitleBar.MaximizeClicked += OnMaximizeClick;
        TitleBar.MinimizeClicked += OnMinimizeClick;
        TitleBar.CloseClicked += OnCloseClick;
        TitleBar.TitleBarDragged += OnTitleBarDrag;

        InitializeComponent();
    }

    ~MainWindow()
    {
        TitleBar.MaximizeClicked -= OnMaximizeClick;
        TitleBar.MinimizeClicked -= OnMinimizeClick;
        TitleBar.CloseClicked -= OnCloseClick;
        TitleBar.TitleBarDragged -= OnTitleBarDrag;
    }

    #region TitleBar Methods

    private void OnMaximizeClick(object? sender, RoutedEventArgs e)
    {
        WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
    }

    private void OnMinimizeClick(object? sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }

    private void OnCloseClick(object? sender, RoutedEventArgs e)
    {
        Close();
    }

    private void OnTitleBarDrag(object? sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton is MouseButton.Left)
        {
            GetWindow(this)?.DragMove();
        }
    }

    #endregion TitleBar Methods

    #region Resize Methods

    private void ResizeGrid_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        _isResizing = true;
    }

    private void ResizeGrid_OnMouseMove(object sender, MouseEventArgs e)
    {
        if (!_isResizing)
        {
            return;
        }

        double x = e.GetPosition(this).X;
        if (x > 0)
        {
            Width = x;
        }
    }

    private void ResizeGrid_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        _isResizing = false;
    }

    #endregion Resize Methods
}