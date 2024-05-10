using System.ComponentModel;
using SportCenter.Utils;
using SportCenter.Views.Components;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using SportCenter.State.Modals;
using WpfScreenHelper;

namespace SportCenter.Views;

public sealed partial class MainWindow : Window
{
    public static event EventHandler? MainSourceInitialized = delegate { };
    public static event EventHandler<bool>? MaximizeFired = delegate { };

    private const string CornerRadiusResource = "WindowCornerRadius";
    private const string MarginResource = "WindowMargin";
    private const double CloseDurationSeconds = 0.1;

    private readonly DispatcherTimer _closeTimer = new()
    {
        Interval = TimeSpan.FromSeconds(CloseDurationSeconds)
    };

    #region Constructor and Destructor

    public MainWindow(object dataContext, IModalService modalService)
    {
        InitializeComponent();

        DataContext = dataContext;

        MaximizeFired += MainWindow_MaximizeFired;

        TitleBar.MaximizeClicked += OnMaximizeClick;
        TitleBar.MinimizeClicked += OnMinimizeClick;
        TitleBar.CloseClicked += OnCloseClick;
        TitleBar.TitleBarDragged += OnTitleBarDrag;

        modalService.ShowModal += ShowModal;
        modalService.HideModal += HideModal;

        InitializeBlurEffectRadiusBinding();
    }

    ~MainWindow()
    {
        MaximizeFired -= MainWindow_MaximizeFired;

        TitleBar.MaximizeClicked -= OnMaximizeClick;
        TitleBar.MinimizeClicked -= OnMinimizeClick;
        TitleBar.CloseClicked -= OnCloseClick;
        TitleBar.TitleBarDragged -= OnTitleBarDrag;

        //LoginModalView.OnModalShown -= ShowLoginModal;
        //LoginModalView.OnModalHidden -= HideLoginModal;
    }

    #endregion Constructor and Destructor

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

            MaximizeFired?.Invoke(sender, true);
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

        AnimationManager.AnimateExpandFadeIn(this);
    }

    private void MainWindow_MaximizeFired(object? sender, bool isMaximized)
    {
        AnimationManager.AnimateMaximizeWindow(this, RenderScale);
        ResizeMode = isMaximized ? ResizeMode.NoResize : ResizeMode.CanResize;
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        e.Cancel = true;

        AnimationManager.AnimateShrinkFadeOut(this);

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
            WindowStateManager.SetWindowSizeToNormal(this);
        }
        else
        {
            WindowStateManager.SetWindowMaximized(this);
        }

        MaximizeFired?.Invoke(sender, WindowStateManager.IsMaximized);
    }

    private void OnMinimizeClick(object? sender, RoutedEventArgs e)
    {
        SystemCommands.MinimizeWindow(this);
    }

    private void OnCloseClick(object? sender, RoutedEventArgs e) => Close();

    private void OnTitleBarDrag(object? sender, MouseEventArgs e)
    {
        if (WindowStateManager.IsMaximized)
        {
            WindowStateManager.SetWindowSizeToNormal(this, true);
            UpdateOuterBorderCornerAndMargin(WindowState is WindowState.Normal);
            MaximizeFired?.Invoke(sender, false);
        }

        if (e.LeftButton is not MouseButtonState.Pressed)
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

    #region Blur Effect Methods

    private void InitializeBlurEffectRadiusBinding()
    {
        DependencyPropertyDescriptor? multiBindingDescriptor = DependencyPropertyDescriptor
            .FromProperty(TagProperty, typeof(Window));

        multiBindingDescriptor.AddValueChanged(this, (_, _) 
            => BlurEffect.Radius = (double)Tag);
    }

    private void ShowBlurEffect(Action? onComplete = null)
    {
        var targetRadius = (double)FindResource("BlurStrength");
        AnimationManager.AnimateBlurEffect(BlurEffect, 0, targetRadius,
            TimeSpan.FromSeconds(0.2), onComplete);
    }

    private void HideBlurEffect(Action? onComplete = null)
    {
        var initialRadius = (double)FindResource("BlurStrength");
        AnimationManager.AnimateBlurEffect(BlurEffect, initialRadius, 0, 
            TimeSpan.FromSeconds(0.2), onComplete);
    }

    #endregion Blur Effect Methods

    #region Modal Methods

    private void ShowModal()
    {
        ModalFrame.IsEnabled = true;
        ModalFrame.Visibility = Visibility.Visible;

        AnimationManager.AnimateExpandFadeIn(ModalFrame);
        ShowBlurEffect();
    }

    private void HideModal()
    {
        ModalFrame.IsEnabled = false;
        ModalFrame.Visibility = Visibility.Collapsed;

        if (ModalFrame is LoginModalView loginModalView)
        {
            loginModalView.InputBlocker.Visibility = Visibility.Collapsed;
        }

        AnimationManager.AnimateShrinkFadeOut(ModalFrame);
        HideBlurEffect();
    }

    #endregion Modal Methods
}