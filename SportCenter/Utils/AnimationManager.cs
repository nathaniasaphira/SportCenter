using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace SportCenter.Utils;

public static class AnimationManager
{
    public static void AnimateExpandFadeIn(FrameworkElement element, ScaleTransform windowRenderScale,
        DependencyProperty opacityProperty, double minScale = 0.9, double duration = 0.1)
    {
        element.RenderTransformOrigin = new Point(0.5, 0.5);

        DoubleAnimation scaleXAnimation = new(minScale, 1, TimeSpan.FromSeconds(duration));
        DoubleAnimation scaleYAnimation = new(minScale, 1, TimeSpan.FromSeconds(duration));
        DoubleAnimation opacityAnimation = new(0, 1, TimeSpan.FromSeconds(duration));

        windowRenderScale.BeginAnimation(ScaleTransform.ScaleXProperty, scaleXAnimation);
        windowRenderScale.BeginAnimation(ScaleTransform.ScaleYProperty, scaleYAnimation);
        element.BeginAnimation(opacityProperty, opacityAnimation);
    }

    public static void AnimateShrinkFadeOut(FrameworkElement element, ScaleTransform windowRenderScale,
        DependencyProperty opacityProperty, Task? onComplete = null, double minScale = 0.9)
    {
        element.RenderTransformOrigin = new Point(0.5, 0.5);

        DoubleAnimation scaleXAnimation = new(1, minScale, TimeSpan.FromSeconds(0.1));
        DoubleAnimation scaleYAnimation = new(1, minScale, TimeSpan.FromSeconds(0.1));
        DoubleAnimation opacityAnimation = new(1, 0, TimeSpan.FromSeconds(0.1));

        opacityAnimation.Completed += (_, _) =>
        {
            onComplete?.Start();
        };

        windowRenderScale.BeginAnimation(ScaleTransform.ScaleXProperty, scaleXAnimation);
        windowRenderScale.BeginAnimation(ScaleTransform.ScaleYProperty, scaleYAnimation);
        element.BeginAnimation(opacityProperty, opacityAnimation);
    }

    public static void AnimateFadeOut(FrameworkElement element, DependencyProperty opacityProperty,
        Task? onComplete = null)
    {
        DoubleAnimation opacityAnimation = new(1, 0, TimeSpan.FromSeconds(0.1));

        opacityAnimation.Completed += (_, _) =>
        {
            onComplete?.Start();
        };

        element.BeginAnimation(opacityProperty, opacityAnimation);
    }

    public static void AnimateMaximizeWindow(FrameworkElement element, ScaleTransform windowRenderScale,
        DependencyProperty opacityProperty, double minScale = 0.95)
    {
        element.RenderTransformOrigin = new Point(0.5, 0.5);

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

    public static void AnimateBounce(FrameworkElement element, ScaleTransform windowRenderScale, 
        int repeatCount = 1)
    {
        element.RenderTransformOrigin = new Point(0.5, 0.5);

        DoubleAnimation scaleXAnimation = new(1, 1.1, TimeSpan.FromSeconds(0.1))
        {
            AutoReverse = true,
            RepeatBehavior = new RepeatBehavior(repeatCount)
        };

        DoubleAnimation scaleYAnimation = new(1, 1.1, TimeSpan.FromSeconds(0.1))
        {
            AutoReverse = true,
            RepeatBehavior = new RepeatBehavior(repeatCount)
        };

        windowRenderScale.BeginAnimation(ScaleTransform.ScaleXProperty, scaleXAnimation);
        windowRenderScale.BeginAnimation(ScaleTransform.ScaleYProperty, scaleYAnimation);
    }
}
