using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SportCenter.Services.Navigators;

namespace SportCenter.Views.Components;

public partial class HomeButtonPrimary : UserControl
{
    public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(HomeButtonPrimary),
            new PropertyMetadata(null));

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public static readonly DependencyProperty CommandParameterProperty =
        DependencyProperty.Register(nameof(CommandParameter), typeof(ViewType), typeof(HomeButtonPrimary),
            new PropertyMetadata(null));

    public ViewType CommandParameter
    {
        get => (ViewType)GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    public static readonly DependencyProperty ButtonTextProperty =
        DependencyProperty.Register(nameof(ButtonText), typeof(string), typeof(HomeButtonPrimary),
            new PropertyMetadata(string.Empty));

    public string ButtonText
    {
        get => (string)GetValue(ButtonTextProperty);
        set => SetValue(ButtonTextProperty, value);
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