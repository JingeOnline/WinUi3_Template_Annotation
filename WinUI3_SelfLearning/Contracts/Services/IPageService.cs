namespace WinUI3_SelfLearning.Contracts.Services;

public interface IPageService
{
    /// <summary>
    /// 根据ViewModel的名称获取对应的View的类型
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    Type GetPageType(string key);
}
