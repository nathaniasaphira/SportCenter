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

        DoubleAnimation scaleXAnimation = new(minScale, 1, TimeSpan.FromSeconds(0.1));
        DoubleAnimation scaleYAnimation = new(minScale, 1, TimeSpan.FromSeconds(0.1));
        DoubleAnimation opacityAnimation = new(0, 1, TimeSpan.FromSeconds(0.1));

        windowRenderScale.BeginAnimation(ScaleTransform.ScaleXProperty, scaleXAnimation);
        windowRenderScale.BeginAnimation(ScaleTransform.ScaleYProperty, scaleYAnimation);
        window.BeginAnimation(opacityProperty, opacityAnimation);
    }

    public static void AnimateShrinkFadeOut(Window window, ScaleTransform windowRenderScale,
        DependencyProperty opacityProperty, Task? onComplete = null, double minScale = 0.9)
    {
        window.RenderTransformOrigin = new Point(0.5, 0.5);

        DoubleAnimation scaleXAnimation = new(1, minScale, TimeSpan.FromSeconds(0.1));
        DoubleAnimation scaleYAnimation = new(1, minScale, TimeSpan.FromSeconds(0.1));
        DoubleAnimation opacityAnimation = new(1, 0, TimeSpan.FromSeconds(0.1));

        opacityAnimation.Completed += (_, _) =>
        {
            onComplete?.Start();
        };

        windowRenderScale.BeginAnimation(ScaleTransform.ScaleXProperty, scaleXAnimation);
        windowRenderScale.BeginAnimation(ScaleTransform.ScaleYProperty, scaleYAnimation);
        window.BeginAnimation(opacityProperty, opacityAnimation);
    }

    public static void AnimateMaximizeWindow(Window window, ScaleTransform windowRenderScale,
        DependencyProperty opacityProperty, double minScale = 0.95)
    {
        window.RenderTransformOrigin = new Point(0.5, 0.5);

        DoubleAnimation scaleXAnimation = new(minScale, 1, TimeSpan.FromSeconds(0.1))
        {
            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
        };

        DoubleAnimation scaleYAnimation = new(minScale, 1, TimeSpan.FromSeconds(0.1))
        {
            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
        };

        windowRenderScale.BeginAnimation(ScaleTransform.ScaleXProperty, scaleXAnimation);
        windowRenderScale.BeginAnimation(ScaleTransform.ScaleYProperty, scaleYAnimation);
    }
}
