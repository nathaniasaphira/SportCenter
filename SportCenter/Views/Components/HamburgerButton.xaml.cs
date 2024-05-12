using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SportCenter.Views.Components;

public partial class HamburgerButton : UserControl
{
    public static readonly DependencyProperty CommandProperty = 
        DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(HamburgerButton),
            new PropertyMetadata(null));

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public static readonly DependencyProperty CommandParameterProperty =
        DependencyProperty.Register(nameof(CommandParameter), typeof(object), typeof(HamburgerButton),
            new PropertyMetadata(null));

    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    public static readonly DependencyProperty IsCheckedProperty =
        DependencyProperty.Register(nameof(IsChecked), typeof(bool), typeof(HamburgerButton), 
            new PropertyMetadata(false));

    public bool IsChecked
    {
        get => (bool)GetValue(IsCheckedProperty);
        set => SetValue(IsCheckedProperty, value);
    }

    public HamburgerButton()
    {
        InitializeComponent();
    }
}