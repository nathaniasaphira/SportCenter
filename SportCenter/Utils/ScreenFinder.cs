using System.Windows;
using System.Windows.Input;
using WpfScreenHelper;

namespace SportCenter.Utils;

public static class ScreenFinder
{
    public static Screen? FindAppropriateScreen(Window window)
    {
        var mousePosition = Mouse.GetPosition(window);
        var screenPoint = window.PointToScreen(mousePosition);
        var screenWithMouse = Screen.AllScreens.FirstOrDefault(screen => screen.Bounds.Contains(screenPoint));

        if (screenWithMouse != null)
        {
            return screenWithMouse;
        }

        double windowRight = window.Left + window.Width;
        double windowBottom = window.Top + window.Height;

        List<Screen> allScreens = Screen.AllScreens.ToList();

        Screen? screenInsideAllBounds = allScreens.Find(x => window.Top >= x.Bounds.Top
                                                             && window.Left >= x.Bounds.Left
                                                             && windowRight <= x.Bounds.Right
                                                             && windowBottom <= x.Bounds.Bottom);
        if (screenInsideAllBounds != null)
        {
            return screenInsideAllBounds;
        }

        List<Screen> screensInBounds = allScreens.FindAll(x => window.Top >= x.Bounds.Top
                                                               && windowBottom <= x.Bounds.Bottom);

        if (screensInBounds.Count <= 0)
        {
            return Screen.PrimaryScreen;
        }

        List<Tuple<double, Screen>> values = [];
        foreach (var screen in screensInBounds.OrderBy(x => x.Bounds.Left))
        {
            double amountInScreen;
            if (screen.Bounds.Left == 0)
            {
                var rightOfWindow = window.Left + window.Width;
                var outsideRightBoundary = rightOfWindow - screen.Bounds.Right;
                amountInScreen = window.Width - outsideRightBoundary;
                values.Add(new Tuple<double, Screen>(amountInScreen, screen));
            }
            else
            {
                var outsideLeftBoundary = screen.Bounds.Left - window.Left;
                amountInScreen = window.Width - outsideLeftBoundary;
                values.Add(new Tuple<double, Screen>(amountInScreen, screen));
            }
        }

        values = values.OrderByDescending(x => x.Item1).ToList();

        return values.Count <= 0 ? Screen.PrimaryScreen : values[0].Item2;
    }
}