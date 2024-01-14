namespace WinUI3_SelfLearning.Behaviors;


/// <summary>
/// 该属性可以设置在子页面xaml文件的Page属性中，比如：behaviors:NavigationViewHeaderBehavior.HeaderMode="Never"
/// </summary>
public enum NavigationViewHeaderMode
{
    //总是在子页面的顶部显式标题
    Always,
    //不在子页面的顶部显式标题
    Never,
    //只有在应用程序被拉伸到很窄的情况下才显式标题
    Minimal
}
