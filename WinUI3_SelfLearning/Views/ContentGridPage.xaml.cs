using Microsoft.UI.Xaml.Controls;

using WinUI3_SelfLearning.ViewModels;

namespace WinUI3_SelfLearning.Views;

public sealed partial class ContentGridPage : Page
{
    public ContentGridViewModel ViewModel
    {
        get;
    }

    public ContentGridPage()
    {
        ViewModel = App.GetService<ContentGridViewModel>();
        InitializeComponent();
    }
}
