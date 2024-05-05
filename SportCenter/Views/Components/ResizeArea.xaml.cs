using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SportCenter.Views.Components;

public partial class ResizeArea : UserControl
{
    public static event EventHandler? OnResize;

    private enum ResizeDirection
    {
        Left = 61441,
        Right = 61442,
        Top = 61443,
        TopLeft = 61444,
        TopRight = 61445,
        Bottom = 61446,
        BottomLeft = 61447,
        BottomRight = 61448,
    }

    private const int ResizeSysCommand = 0x112;

    private HwndSource? _hWndSource;

    public ResizeArea()
    {
        InitializeComponent();

        MainWindow.MainSourceInitialized += Window_SourceInitialized!;
        MainWindow.MaximizeFired += Window_MaximizeFired!;
    }

    ~ResizeArea()
    {
        MainWindow.MainSourceInitialized -= Window_SourceInitialized!;
        MainWindow.MaximizeFired -= Window_MaximizeFired!;
    }

    private void Window_SourceInitialized(object sender, EventArgs e)
    {
        _hWndSource = PresentationSource.FromVisual((Visual) sender) as HwndSource;
    }

    private void Window_MaximizeFired(object sender, WindowState e)
    {
        IsEnabled = e is not WindowState.Maximized;
    }

    private void ResetCursor(object sender, MouseEventArgs e)
    {
        if (Mouse.LeftButton != MouseButtonState.Pressed)
        {
            Cursor = Cursors.Arrow;
        }
    }

    private void Resize(object sender, MouseButtonEventArgs e)
    {
        if (sender is not Shape clickedShape)
        {
            return;
        }

        (Cursor, ResizeDirection direction) = clickedShape.Name switch
        {
            "ResizeN" => (Cursors.SizeNS, ResizeDirection.Top),
            "ResizeE" => (Cursors.SizeWE, ResizeDirection.Right),
            "ResizeS" => (Cursors.SizeNS, ResizeDirection.Bottom),
            "ResizeW" => (Cursors.SizeWE, ResizeDirection.Left),
            "ResizeNW" => (Cursors.SizeNWSE, ResizeDirection.TopLeft),
            "ResizeNE" => (Cursors.SizeNESW, ResizeDirection.TopRight),
            "ResizeSE" => (Cursors.SizeNWSE, ResizeDirection.BottomRight),
            "ResizeSW" => (Cursors.SizeNESW, ResizeDirection.BottomLeft),
            _ => (Cursor, default)
        };

        if (Cursor == null)
        {
            return;
        }

        ResizeWindow(direction);
        OnResize?.Invoke(this, EventArgs.Empty);
    }

    private void ResizeWindow(ResizeDirection direction)
    {
        SendMessage(_hWndSource!.Handle, ResizeSysCommand, (IntPtr) direction);
    }

    [DllImport("user32.dll")]
    private static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam);

    private void DisplayResizeCursor(object sender, MouseEventArgs e)
    {
        if (sender is not Shape clickedShape)
        {
            return;
        }

        Cursor = clickedShape.Name switch
        {
            "ResizeN" or "ResizeS" => Cursors.SizeNS,
            "ResizeE" or "ResizeW" => Cursors.SizeWE,
            "ResizeNW" or "ResizeSE" => Cursors.SizeNWSE,
            "ResizeNE" or "ResizeSW" => Cursors.SizeNESW,
            _ => Cursor
        };
    }
}