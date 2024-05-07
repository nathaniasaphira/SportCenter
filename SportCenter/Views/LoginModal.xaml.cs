using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SportCenter.Utils;
using SportCenter.ViewModels;

namespace SportCenter.Views;

public partial class LoginModal : UserControl
{
    private LoginModalViewModel ViewModel { get; }

    public LoginModal()
    {
        InitializeComponent();
        
        ViewModel = new LoginModalViewModel();
        DataContext = ViewModel;
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
        AnimationManager.AnimateExpandFadeIn(this, RenderScale, OpacityProperty, duration: 0.2);
    }

    private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
    {
        if (sender is PasswordBox passwordBox)
        {
            ViewModel.Password = passwordBox.Password;
        }
    }
}