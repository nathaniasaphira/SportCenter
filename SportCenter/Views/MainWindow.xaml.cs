using System.ComponentModel;
using SportCenter.Utils;
using SportCenter.Views.Components;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using SportCenter.Services.Modals;
using SportCenter.Services.Navigators;
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

    private readonly IModalService _modalService;
    private readonly ModalNavigator _modalNavigator;

    #region Constructor and Destructor

    public MainWindow(IModalService modalService, ModalNavigator modalNavigator)
    {
        InitializeComponent();

        _modalService = modalService;
        _modalNavigator = modalNavigator;

        MaximizeFired += MainWindow_MaximizeFired;

        TitleBar.MaximizeClicked += OnMaximizeClick;
        TitleBar.MinimizeClicked += OnMinimizeClick;
        TitleBar.CloseClicked += OnCloseClick;
        TitleBar.TitleBarDragged += OnTitleBarDrag;

        _modalService.ShowModal += ShowModal;
        _modalService.HideModal += HideModal;
        _modalNavigator.ViewModelStateChanged += OnModalViewModelChanged;

        if (!modalNavigator.IsModalOpen)
        {
            HideBlurEffect();
        }
    }

    ~MainWindow()
    {
        MaximizeFired -= MainWindow_MaximizeFired;

        TitleBar.MaximizeClicked -= OnMaximizeClick;
        TitleBar.MinimizeClicked -= OnMinimizeClick;
        TitleBar.CloseClicked -= OnCloseClick;
        TitleBar.TitleBarDragged -= OnTitleBarDrag;

        _modalService.ShowModal -= ShowModal;
        _modalService.HideModal -= HideModal;
        _modalNavigator.ViewModelStateChanged -= OnModalViewModelChanged;
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
        AnimationManager.AnimateMaximizeWindow(this);
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

        BackgroundBorder.CornerRadius = isMaximized
            ? (CornerRadius)FindResource(CornerRadiusResource)
            : new CornerRadius();
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

    private bool IsBlurEffectVisible() => BlurEffect.Radius is not 0;

    #endregion Blur Effect Methods

    #region Modal Methods

    private void ShowModal(ModalType modalType)
    {
        if (!IsBlurEffectVisible())
        {
            ShowBlurEffect();
        }

        ModalControl.IsEnabled = true;
        ModalControl.Visibility = Visibility.Visible;

        AnimationManager.AnimateExpandFadeIn(ModalControl);
    }

    private void HideModal()
    {
        if (IsBlurEffectVisible())
        {
            HideBlurEffect();
        }

        ModalControl.IsEnabled = false;
        ModalControl.Visibility = Visibility.Collapsed;

        if (ModalControl is LoginModalView loginModalView)
        {
            loginModalView.InputBlocker.Visibility = Visibility.Collapsed;
        }

        AnimationManager.AnimateShrinkFadeOut(ModalControl);
    }

    private void OnModalViewModelChanged()
    {
        ModalControl.Content = _modalNavigator.CurrentViewModel;
    }

    #endregion Modal Methods
}