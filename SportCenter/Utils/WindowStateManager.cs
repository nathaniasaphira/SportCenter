using System.Windows;
using WpfScreenHelper;

namespace SportCenter.Utils;

public static class WindowStateManager
{
    private static double Top { get; set; }
    private static double Left { get; set; }
    private static double Width { get; set; }
    private static double Height { get; set; }

    public static bool IsMaximized { get; private set; }
    public static bool BlockStateChange { get; set; }

    private static void SetWindowTop(Window window)
    {
        BlockStateChange = true;
        window.Top = Top;
    }

    private static void SetWindowLeft(Window window)
    {
        BlockStateChange = true;
        window.Left = Left;
    }

    private static void SetWindowWidth(FrameworkElement window)
    {
        BlockStateChange = true;
        window.Width = Width;
    }

    private static void SetWindowHeight(FrameworkElement window)
    {
        BlockStateChange = true;
        window.Height = Height;
    }

    public static void UpdateLastKnownLocation(double top, double left)
    {
        Top = top;
        Left = left;
    }

    public static void UpdateLastKnownNormalSize(double width, double height)
    {
        Width = width;
        Height = height;
    }

    public static void SetWindowMaximized(Window window)
    {
        window.WindowState = WindowState.Maximized;
        IsMaximized = true;
        window.WindowState = WindowState.Normal;
    }

    private static double MousePercentageFromLeft(Window window)
    {
        double mouseMinusZeroToLeft = MouseHelper.MousePosition.X - window.Left;
        double percentage = mouseMinusZeroToLeft / window.Width;
        return percentage;
    }

    public static void SetWindowSizeToNormal(Window window, bool isDragging = false)
    {
        IsMaximized = false;

        double percentage = MousePercentageFromLeft(window);

        SetWindowWidth(window);
        SetWindowHeight(window);

        if (isDragging)
        {
            Top = MouseHelper.MousePosition.Y - 40;

            double valueOnNewSize = percentage * Width;
            Left = MouseHelper.MousePosition.X - valueOnNewSize;
        }

        SetWindowTop(window);
        SetWindowLeft(window);

        window.WindowState = WindowState.Normal;
    }
}