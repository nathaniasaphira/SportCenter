using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SportCenter.Views.Components;

public partial class TitleBar : UserControl
{
    public static event RoutedEventHandler? MaximizeClicked;
    public static event RoutedEventHandler? MinimizeClicked;
    public static event RoutedEventHandler? CloseClicked;

    public static event MouseButtonEventHandler? TitleBarDragged;

    public TitleBar()
    {
        InitializeComponent();
    }

    #region Button Callbacks

    private void BtnMaximize_Click(object sender, RoutedEventArgs e)
    {
        MaximizeClicked?.Invoke(this, e);
    }

    private void BtnMinimize_Click(object sender, RoutedEventArgs e)
    {
        MinimizeClicked?.Invoke(this, e);
    }

    private void BtnClose_Click(object sender, RoutedEventArgs e)
    {
        CloseClicked?.Invoke(this, e);
    }

    #endregion

    #region TitleBar Drag

    private void TitleBar_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        TitleBarDragged?.Invoke(this, e);
    }

    #endregion

}