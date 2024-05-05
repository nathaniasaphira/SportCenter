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

    public static void AnimateMinimizeWindow(Window window, ScaleTransform windowRenderScale,
        DependencyProperty opacityProperty, double minScale = 0.95)
    {
        window.RenderTransformOrigin = new Point(0.5, 1); // pivot at the bottom

        var endScale = new Point(0.1, 0.1); // end scale, can be adjusted
        var endPos = new Point(window.Left, SystemParameters.PrimaryScreenHeight); // end position, bottom of the screen

        var scaleXAnimation = new DoubleAnimation(windowRenderScale.ScaleX, endScale.X, TimeSpan.FromSeconds(0.2));
        var scaleYAnimation = new DoubleAnimation(windowRenderScale.ScaleY, endScale.Y, TimeSpan.FromSeconds(0.2));

        var translateXAnimation = new DoubleAnimation(window.Left, endPos.X, TimeSpan.FromSeconds(0.2));
        var translateYAnimation = new DoubleAnimation(window.Top, endPos.Y, TimeSpan.FromSeconds(0.2));

        windowRenderScale.BeginAnimation(ScaleTransform.ScaleXProperty, scaleXAnimation);
        windowRenderScale.BeginAnimation(ScaleTransform.ScaleYProperty, scaleYAnimation);

        window.BeginAnimation(Window.LeftProperty, translateXAnimation);
        window.BeginAnimation(Window.TopProperty, translateYAnimation);
    }

}