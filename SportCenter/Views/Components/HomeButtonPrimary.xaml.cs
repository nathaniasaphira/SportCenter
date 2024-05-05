using System.Windows;
using System.Windows.Controls;

namespace SportCenter.Views.Components;

public partial class HomeButtonPrimary : UserControl
{
    public static readonly DependencyProperty ButtonTextProperty =
        DependencyProperty.Register(nameof(ButtonText), typeof(string), typeof(HomeButtonPrimary),
            new PropertyMetadata(string.Empty));

    public string ButtonText
    {
        get => (string)GetValue(ButtonTextProperty);
        set => SetValue(ButtonTextProperty, value);
    }

    public static readonly DependencyProperty ImageSourceProperty = 
        DependencyProperty.Register(nameof(ImageSource), typeof(string), typeof(HomeButtonPrimary), 
            new PropertyMetadata(string.Empty));

    public string ImageSource
    {
        get => (string)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }

    public static readonly DependencyProperty SvgImageSourceProperty =
        DependencyProperty.Register(nameof(SvgImageSource), typeof(string), typeof(HomeButtonPrimary),
            new PropertyMetadata(string.Empty));

    public string SvgImageSource
    {
        get => (string)GetValue(SvgImageSourceProperty);
        set => SetValue(SvgImageSourceProperty, value);
    }

    public HomeButtonPrimary()
    {
        InitializeComponent();
    }
}