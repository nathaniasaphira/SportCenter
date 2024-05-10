using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Threading;

namespace SportCenter.Utils;

public static class AnimationManager
{
    public static void AnimateExpandFadeIn(FrameworkElement element, double minScale = 0.9, double duration = 0.1)
    {
        ScaleTransform renderScale = element.RenderTransform as ScaleTransform ?? new ScaleTransform();
        element.RenderTransformOrigin = new Point(0.5, 0.5);

        DoubleAnimation scaleXAnimation = new(minScale, 1, TimeSpan.FromSeconds(duration));
        DoubleAnimation scaleYAnimation = new(minScale, 1, TimeSpan.FromSeconds(duration));
        DoubleAnimation opacityAnimation = new(0, 1, TimeSpan.FromSeconds(duration));

        renderScale.BeginAnimation(ScaleTransform.ScaleXProperty, scaleXAnimation);
        renderScale.BeginAnimation(ScaleTransform.ScaleYProperty, scaleYAnimation);
        element.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
    }

    public static void AnimateShrinkFadeOut(FrameworkElement element, Task? onComplete = null,
        double minScale = 0.9)
    {
        ScaleTransform renderScale = element.RenderTransform as ScaleTransform ?? new ScaleTransform();
        element.RenderTransformOrigin = new Point(0.5, 0.5);

        DoubleAnimation scaleXAnimation = new(1, minScale, TimeSpan.FromSeconds(0.1));
        DoubleAnimation scaleYAnimation = new(1, minScale, TimeSpan.FromSeconds(0.1));
        DoubleAnimation opacityAnimation = new(1, 0, TimeSpan.FromSeconds(0.1));

        scaleXAnimation.Completed += (_, _) =>
        {
            onComplete?.Start();
        };

        renderScale.BeginAnimation(ScaleTransform.ScaleXProperty, scaleXAnimation);
        renderScale.BeginAnimation(ScaleTransform.ScaleYProperty, scaleYAnimation);
        element.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
    }

    public static void AnimateBlurEffect(BlurEffect blurEffect, double from, double to, TimeSpan duration, Action? onComplete = null)
    {
        double startValue = from;
        double endValue = to;

        DateTime startTime = DateTime.Now;
        DispatcherTimer timer = new() { Interval = TimeSpan.FromMilliseconds(10) };

        timer.Tick += (_, _) =>
        {
            double progress = (DateTime.Now - startTime).TotalSeconds / duration.TotalSeconds;
            blurEffect.Radius = startValue + (endValue - startValue) * progress;

            if (progress < 1)
            {
                return;
            }

            blurEffect.Radius = endValue;
            timer.Stop();

            onComplete?.Invoke();
        };

        timer.Start();
    }

    public static void AnimateMaximizeWindow(FrameworkElement element, ScaleTransform windowRenderScale, double minScale = 0.95)
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
