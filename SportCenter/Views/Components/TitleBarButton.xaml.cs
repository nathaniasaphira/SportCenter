using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SportCenter.Views.Components;

public partial class TitleBarButton : UserControl
{
    public event RoutedEventHandler? ButtonClicked;

    public static readonly DependencyProperty ButtonBackgroundProperty =
        DependencyProperty.Register(nameof(ButtonBackground), typeof(Brush), 
            typeof(TitleBarButton), new PropertyMetadata(Brushes.Transparent));

    public Brush ButtonBackground
    {
        get => (Brush)GetValue(ButtonBackgroundProperty);
        set => SetValue(ButtonBackgroundProperty, value);
    }

    public static readonly DependencyProperty ButtonGeometryProperty =
        DependencyProperty.Register(nameof(ButtonGeometry), typeof(Geometry), typeof(TitleBarButton),
            new PropertyMetadata(null));

    public Geometry ButtonGeometry
    {
        get => (Geometry)GetValue(ButtonGeometryProperty);
        set => SetValue(ButtonGeometryProperty, value);
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