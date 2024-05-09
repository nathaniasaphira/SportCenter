using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SportCenter.Utils;
using SportCenter.ViewModels;

namespace SportCenter.Views;

public partial class LoginModalView : UserControl
{
    public static event Action? OnModalShown = delegate { };
    public static event Action? OnModalHidden = delegate { };

    private LoginModalViewModel LoginModalViewModel { get; } = new();

    public LoginModalView()
    {
        InitializeComponent();

        DataContext = LoginModalViewModel;

        LoginModalViewModel.ShowModalAction += ShowModal;
        LoginModalViewModel.CloseModalAction += HideModal;
    }

    ~LoginModalView()
    {
        LoginModalViewModel.ShowModalAction -= ShowModal;
        LoginModalViewModel.CloseModalAction -= HideModal;
    }

    private void ShowModal()
    {
        IsEnabled = true;
        InputBlocker.Visibility = Visibility.Visible;

        AnimationManager.AnimateExpandFadeIn(this, 
            RenderScale, OpacityProperty);

        OnModalShown?.Invoke();
    }

    private void HideModal()
    {
        IsEnabled = false;
        InputBlocker.Visibility = Visibility.Collapsed;

        AnimationManager.AnimateShrinkFadeOut(this, 
            RenderScale, OpacityProperty,
            onComplete: new Task(() =>
            {
                Visibility = Visibility.Collapsed;
            }));

        OnModalHidden?.Invoke();
    }

    private void LoginModal_OnLoaded(object sender, RoutedEventArgs e)
    {
        PlayEnterAnimation();
    }

    private void InputBlocker_OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton is MouseButton.Left)
        {
            AnimationManager.AnimateBounce(this, RenderScale);
        }
    }

    private async void PlayEnterAnimation()
    {
        Opacity = 0;
        await Task.Delay(TimeSpan.FromSeconds(0.2));
        AnimationManager.AnimateExpandFadeIn(this, 
            RenderScale, OpacityProperty);
    }

    private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
    {
        if (sender is not PasswordBox passwordBox)
        {
            return;
        }

        if (DataContext is not LoginModalViewModel viewModel)
        {
            return;
        }

        viewModel.Password = passwordBox.Password;
    }
}