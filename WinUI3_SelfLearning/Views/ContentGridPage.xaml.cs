using Microsoft.UI.Xaml.Controls;

using WinUI3_SelfLearning.ViewModels;

namespace WinUI3_SelfLearning.Views;

public sealed partial class ContentGridPage : Page
{
    //在View中声明一个ViewModel类型的变量
    public ContentGridViewModel ViewModel
    {
        get;
    }

    public ContentGridPage()
    {
        //通过容器来获取ViewModel的实例
        ViewModel = App.GetService<ContentGridViewModel>();
        InitializeComponent();
    }
}
