using CommunityToolkit.Mvvm.ComponentModel;

using Microsoft.UI.Xaml.Controls;

using WinUI3_SelfLearning.Contracts.Services;
using WinUI3_SelfLearning.ViewModels;
using WinUI3_SelfLearning.Views;

namespace WinUI3_SelfLearning.Services;

public class PageService : IPageService
{
    private readonly Dictionary<string, Type> _pages = new();

    public PageService()
    {
        Configure<MainViewModel, MainPage>();
        Configure<ListDetailsViewModel, ListDetailsPage>();
        Configure<ContentGridViewModel, ContentGridPage>();
        Configure<ContentGridDetailViewModel, ContentGridDetailPage>();
        Configure<DataGridViewModel, DataGridPage>();
        Configure<SettingsViewModel, SettingsPage>();
    }

    /// <summary>
    /// 根据ViewModel的名称获取对应的View的类型
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public Type GetPageType(string key)
    {
        Type? pageType;
        lock (_pages)
        {
            if (!_pages.TryGetValue(key, out pageType))
            {
                throw new ArgumentException($"Page not found: {key}. Did you forget to call PageService.Configure?");
            }
        }

        return pageType;
    }

    /// <summary>
    /// 把每个页面的ViewModel和View作为键值对，添加到字典中。
    /// </summary>
    /// <typeparam name="VM"></typeparam>
    /// <typeparam name="V"></typeparam>
    /// <exception cref="ArgumentException"></exception>
    private void Configure<VM, V>()
        where VM : ObservableObject
        where V : Page
    {
        lock (_pages)
        {
            var key = typeof(VM).FullName!;
            if (_pages.ContainsKey(key))
            {
                throw new ArgumentException($"The key {key} is already configured in PageService");
            }

            var type = typeof(V);
            if (_pages.ContainsValue(type))
            {
                throw new ArgumentException($"This type is already configured with key {_pages.First(p => p.Value == type).Key}");
            }

            _pages.Add(key, type);
        }
    }
}
