using Microsoft.Windows.ApplicationModel.Resources;

namespace WinUI3_SelfLearning.Helpers;

public static class ResourceExtensions
{
    private static readonly ResourceLoader _resourceLoader = new();

    /// <summary>
    /// 获取文本对应的本地语言
    /// </summary>
    /// <param name="resourceKey">文本资源的Key</param>
    /// <returns></returns>
    public static string GetLocalized(this string resourceKey) => _resourceLoader.GetString(resourceKey);
}
