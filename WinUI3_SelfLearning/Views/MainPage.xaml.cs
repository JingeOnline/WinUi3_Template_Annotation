using Microsoft.UI.Xaml.Controls;

using WinUI3_SelfLearning.ViewModels;

namespace WinUI3_SelfLearning.Views;

public sealed partial class MainPage : Page
{
    public MainViewModel ViewModel
    {
        get;
    }

    public MainPage()
    {
        ViewModel = App.GetService<MainViewModel>();
        InitializeComponent();
    }
}
