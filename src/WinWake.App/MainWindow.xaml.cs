using System.Windows;
using WinWake.App.ViewModels;

namespace WinWake.App;

public partial class MainWindow : Window
{
    public MainWindow(MainViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
