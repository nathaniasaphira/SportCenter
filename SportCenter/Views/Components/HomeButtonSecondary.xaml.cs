using System.Windows;
using System.Windows.Controls;

namespace SportCenter.Views.Components;

public partial class HomeButtonSecondary : UserControl
{
    public static readonly DependencyProperty ButtonTextProperty =
        DependencyProperty.Register(nameof(ButtonText), typeof(string), typeof(HomeButtonSecondary),
            new PropertyMetadata(string.Empty));

    public string ButtonText
    {
        get => (string)GetValue(ButtonTextProperty);
        set => SetValue(ButtonTextProperty, value);
    }

    public static readonly DependencyProperty SvgImageSourceProperty =
        DependencyProperty.Register(nameof(SvgImageSource), typeof(string), typeof(HomeButtonSecondary),
            new PropertyMetadata(string.Empty));

    public string SvgImageSource
    {
        get => (string)GetValue(SvgImageSourceProperty);
        set => SetValue(SvgImageSourceProperty, value);
    }

    public HomeButtonSecondary()
    {
        InitializeComponent();
    }
}