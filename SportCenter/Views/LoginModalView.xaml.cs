﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SportCenter.Utils;
using SportCenter.ViewModels;

namespace SportCenter.Views;

public partial class LoginModalView : UserControl
{
    public LoginModalView()
    {
        InitializeComponent();
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
        AnimationManager.AnimateExpandFadeIn(this);
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