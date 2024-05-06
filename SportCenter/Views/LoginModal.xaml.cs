using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SportCenter.Utils;

namespace SportCenter.Views
{
    public partial class LoginModal : UserControl
    {
        public LoginModal()
        {
            InitializeComponent();
        }

        private void LoginModal_OnLoaded(object sender, RoutedEventArgs e)
        {
            AnimationManager.AnimateExpandFadeIn(this, RenderScale, OpacityProperty, duration: 0.4);
        }

        private void InputBlocker_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton is MouseButton.Left)
            {
                AnimationManager.AnimateBounce(this, RenderScale);
            }
        }
    }
}