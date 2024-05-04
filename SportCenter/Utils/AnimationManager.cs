using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace SportCenter.Utils;

public static class AnimationManager
{
    public static void AnimateExpandFadeIn(Window window, ScaleTransform windowRenderScale,
        DependencyProperty opacityProperty, double minScale = 0.9)
    {
        window.RenderTransformOrigin = new Point(0.5, 0.5);

        var scaleXAnimation = new DoubleAnimation(minScale, 1, TimeSpan.FromSeconds(0.1));
        var scaleYAnimation = new DoubleAnimation(minScale, 1, TimeSpan.FromSeconds(0.1));
        var opacityAnimation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.1));

        windowRenderScale.BeginAnimation(ScaleTransform.ScaleXProperty, scaleXAnimation);
        windowRenderScale.BeginAnimation(ScaleTransform.ScaleYProperty, scaleYAnimation);
        window.BeginAnimation(opacityProperty, opacityAnimation);
    }

    public static void AnimateShrinkFadeOut(Window window, ScaleTransform windowRenderScale,
        DependencyProperty opacityProperty, double minScale = 0.9)
    {
        window.RenderTransformOrigin = new Point(0.5, 0.5);

        var scaleXAnimation = new DoubleAnimation(1, minScale, TimeSpan.FromSeconds(0.1));
        var scaleYAnimation = new DoubleAnimation(1, minScale, TimeSpan.FromSeconds(0.1));
        var opacityAnimation = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.1));

        windowRenderScale.BeginAnimation(ScaleTransform.ScaleXProperty, scaleXAnimation);
        windowRenderScale.BeginAnimation(ScaleTransform.ScaleYProperty, scaleYAnimation);
        window.BeginAnimation(opacityProperty, opacityAnimation);
    }

    public static void AnimateMaximizeWindow(Window window, ScaleTransform windowRenderScale,
        DependencyProperty opacityProperty, double minScale = 0.95)
    {
        window.RenderTransformOrigin = new Point(0.5, 0.5);

        var scaleXAnimation = new DoubleAnimation(minScale, 1, TimeSpan.FromSeconds(0.1))
        {
            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
        };

        var scaleYAnimation = new DoubleAnimation(minScale, 1, TimeSpan.FromSeconds(0.1))
        {
            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
        };

        windowRenderScale.BeginAnimation(ScaleTransform.ScaleXProperty, scaleXAnimation);
        windowRenderScale.BeginAnimation(ScaleTransform.ScaleYProperty, scaleYAnimation);
    }
}