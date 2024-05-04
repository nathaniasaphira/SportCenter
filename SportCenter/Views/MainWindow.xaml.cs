using System.ComponentModel;
using SportCenter.Utils;
using SportCenter.Views.Components;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using WpfScreenHelper;

namespace SportCenter.Views;

public partial class MainWindow : Window
{
    public static event EventHandler? MainSourceInitialized;
    public static event EventHandler<WindowState>? MaximizeFired;

    private const string CornerRadiusResource = "WindowCornerRadius";
    private const string MarginResource = "WindowMargin";
    private const double CloseDurationSeconds = 0.1;

    private readonly DispatcherTimer _closeTimer = new()
    {
        Interval = TimeSpan.FromSeconds(CloseDurationSeconds)
    };

    public MainWindow()
    {
        InitializeComponent();

        TitleBar.MaximizeClicked += OnMaximizeClick;
        TitleBar.MinimizeClicked += OnMinimizeClick;
        TitleBar.CloseClicked += OnCloseClick;
        TitleBar.TitleBarDragged += OnTitleBarDrag;
        MaximizeFired += MainWindow_MaximizeFired;
    }

    public void Dispose()
    {
        TitleBar.MaximizeClicked -= OnMaximizeClick;
        TitleBar.MinimizeClicked -= OnMinimizeClick;
        TitleBar.CloseClicked -= OnCloseClick;
        TitleBar.TitleBarDragged -= OnTitleBarDrag;
        MaximizeFired -= MainWindow_MaximizeFired;

        GC.SuppressFinalize(this);
    }

    #region Window Events

    private void MainWindow_OnSourceInitialized(object? sender, EventArgs e)
    {
        MainSourceInitialized?.Invoke(sender, e);
    }

    private void MainWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (WindowState is WindowState.Maximized)
        {
            WindowStateManager.SetWindowMaximized(this);
            WindowStateManager.BlockStateChange = true;

            SetWindowSizeAndPosition();
            UpdateOuterBorderCornerAndMargin(WindowState is WindowState.Maximized);

            MaximizeFired?.Invoke(sender, WindowState.Maximized);
            return;
        }

        if (WindowStateManager.BlockStateChange)
        {
            WindowStateManager.BlockStateChange = false;
            return;
        }

        WindowStateManager.UpdateLastKnownNormalSize(Width, Height);
        WindowStateManager.UpdateLastKnownLocation(Top, Left);
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        if (WindowState is WindowState.Maximized)
        {
            SetWindowSizeAndPosition();
            UpdateOuterBorderCornerAndMargin(true);
        }

        AnimationManager.AnimateExpandFadeIn(this, RenderScale, OpacityProperty);
    }

    private void MainWindow_MaximizeFired(object? sender, WindowState e)
    {
        AnimationManager.AnimateMaximizeWindow(this, RenderScale, OpacityProperty);
        ResizeMode = e is WindowState.Maximized ? ResizeMode.NoResize : ResizeMode.CanResize;
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        e.Cancel = true;

        AnimationManager.AnimateShrinkFadeOut(this, RenderScale, OpacityProperty);

        _closeTimer.Tick += (_, _) =>
        {
            _closeTimer.Stop();

            Application.Current.Shutdown();
        };
        _closeTimer.Start();
    }

    #endregion Window Events

    #region Window State Management

    private void SetWindowSizeAndPosition()
    {
        Screen? screen = ScreenFinder.FindAppropriateScreen(this);

        if (screen == null)
        {
            return;
        }

        Top = screen.WorkingArea.Top;
        Left = screen.WorkingArea.Left;
        Width = screen.WorkingArea.Width;
        Height = screen.WorkingArea.Height;
    }

    private void UpdateOuterBorderCornerAndMargin(bool isMaximized)
    {
        OuterBorder.CornerRadius = isMaximized
            ? (CornerRadius)FindResource(CornerRadiusResource)
            : new CornerRadius();

        OuterBorder.Margin = isMaximized
            ? (Thickness)FindResource(MarginResource)
            : new Thickness(0);
    }

    #endregion Window State Management

    #region Event Handlers

    private void OnMaximizeClick(object? sender, RoutedEventArgs e)
    {
        UpdateOuterBorderCornerAndMargin(WindowStateManager.IsMaximized);

        if (WindowStateManager.IsMaximized)
        {
            WindowState = WindowState.Normal;

            WindowStateManager.SetWindowSizeToNormal(this, true);
            MaximizeFired?.Invoke(sender, WindowState.Normal);

            return;
        }

        WindowState = WindowState.Maximized;

        WindowStateManager.SetWindowMaximized(this);
        MaximizeFired?.Invoke(sender, WindowState.Maximized);
    }

    private void OnMinimizeClick(object? sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

    private void OnCloseClick(object? sender, RoutedEventArgs e) => Close();

    private void OnTitleBarDrag(object? sender, MouseEventArgs e)
    {
        if (WindowStateManager.IsMaximized)
        {
            WindowState = WindowState.Normal;
            WindowStateManager.SetWindowSizeToNormal(this, true);

            UpdateOuterBorderCornerAndMargin(WindowState is WindowState.Normal);
            MaximizeFired?.Invoke(sender, WindowState.Normal);
        }

        if (e.LeftButton != MouseButtonState.Pressed)
        {
            return;
        }

        try
        {
            DragMove();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        WindowStateManager.UpdateLastKnownLocation(Top, Left);
    }

    #endregion Event Handlers
}