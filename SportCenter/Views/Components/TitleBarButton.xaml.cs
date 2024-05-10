using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SportCenter.Views.Components;

public partial class TitleBarButton : UserControl
{
    public event RoutedEventHandler? ButtonClicked = delegate { };

    public static readonly DependencyProperty ButtonBackgroundProperty =
        DependencyProperty.Register(nameof(ButtonBackground), typeof(Brush), 
            typeof(TitleBarButton), new PropertyMetadata(Brushes.Transparent));

    public Brush ButtonBackground
    {
        get => (Brush)GetValue(ButtonBackgroundProperty);
        set => SetValue(ButtonBackgroundProperty, value);
    }

    public static readonly DependencyProperty ButtonPaddingProperty =
        DependencyProperty.Register(nameof(ButtonPadding), typeof(Thickness), typeof(TitleBarButton),
                       new PropertyMetadata(new Thickness(7)));

    public Thickness ButtonPadding
    {
        get => (Thickness)GetValue(ButtonPaddingProperty);
        set => SetValue(ButtonPaddingProperty, value);
    }

    public static readonly DependencyProperty ButtonGeometryProperty =
        DependencyProperty.Register(nameof(ButtonGeometry), typeof(Geometry), typeof(TitleBarButton),
            new PropertyMetadata(null));

    public Geometry ButtonGeometry
    {
        get => (Geometry)GetValue(ButtonGeometryProperty);
        set => SetValue(ButtonGeometryProperty, value);
    }

    public static readonly DependencyProperty IconThicknessProperty =
        DependencyProperty.Register(nameof(IconThickness), typeof(double), typeof(TitleBarButton),
                       new PropertyMetadata(1.3));

    public double IconThickness
    {
        get => (double)GetValue(IconThicknessProperty);
        set => SetValue(IconThicknessProperty, value);
    }

    public TitleBarButton()
    {
        InitializeComponent();
    }

    private void Button_OnClick(object sender, RoutedEventArgs e)
    {
        ButtonClicked?.Invoke(this, e);
    }
}