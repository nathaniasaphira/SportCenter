using System.Windows.Controls;
using SportCenter.ViewModels;

namespace SportCenter.Views;

public partial class HomeView : Page
{
    private HomeViewModel HomeViewModel { get; } = new();

    public HomeView()
    {
        InitializeComponent();

        DataContext = HomeViewModel;
    }
}