using System.Windows;
using System.Windows.Input;
using SportCenter.Views.Components;

namespace SportCenter.Views;

public partial class MainWindow : Window
{
    public static event EventHandler? MainSourceInitialized;

    public MainWindow()
    {
        TitleBar.MaximizeClicked += OnMaximizeClick;
        TitleBar.MinimizeClicked += OnMinimizeClick;
        TitleBar.CloseClicked += OnCloseClick;
        TitleBar.TitleBarDragged += OnTitleBarDrag;

        SourceInitialized += OnSourceInitialized!;

        InitializeComponent();
    }

    ~MainWindow()
    {
        TitleBar.MaximizeClicked -= OnMaximizeClick;
        TitleBar.MinimizeClicked -= OnMinimizeClick;
        TitleBar.CloseClicked -= OnCloseClick;
        TitleBar.TitleBarDragged -= OnTitleBarDrag;

        SourceInitialized -= OnSourceInitialized!;
    }

    private static void OnSourceInitialized(object sender, EventArgs e)
    {
        MainSourceInitialized?.Invoke(sender, e);
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
}