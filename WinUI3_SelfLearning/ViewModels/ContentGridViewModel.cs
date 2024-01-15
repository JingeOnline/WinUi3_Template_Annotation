using System.Collections.ObjectModel;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using WinUI3_SelfLearning.Contracts.Services;
using WinUI3_SelfLearning.Contracts.ViewModels;
using WinUI3_SelfLearning.Core.Contracts.Services;
using WinUI3_SelfLearning.Core.Models;

namespace WinUI3_SelfLearning.ViewModels;

public partial class ContentGridViewModel : ObservableRecipient, INavigationAware
{
    private readonly INavigationService _navigationService;
    private readonly ISampleDataService _sampleDataService;

    public ObservableCollection<SampleOrder> Source { get; } = new ObservableCollection<SampleOrder>();

    public ContentGridViewModel(INavigationService navigationService, ISampleDataService sampleDataService)
    {
        _navigationService = navigationService;
        _sampleDataService = sampleDataService;
    }

    /// <summary>
    /// 导航进入此页面
    /// </summary>
    /// <param name="parameter"></param>
    public async void OnNavigatedTo(object parameter)
    {
        Source.Clear();

        // TODO: Replace with real data.
        var data = await _sampleDataService.GetContentGridDataAsync();
        foreach (var item in data)
        {
            Source.Add(item);
        }
    }

    /// <summary>
    /// 导航离开此页面
    /// </summary>
    public void OnNavigatedFrom()
    {
    }

    //通过使用该Attribute，会自动根据方法的名称，生成对应的Command。
    //比如当前方法，会自动生成一个名为ItemClickCommand的 ICommand对象。
    [RelayCommand]
    private void OnItemClick(SampleOrder? clickedItem)
    {
        if (clickedItem != null)
        {
            _navigationService.SetListDataItemForNextConnectedAnimation(clickedItem);
            _navigationService.NavigateTo(typeof(ContentGridDetailViewModel).FullName!, clickedItem.OrderID);
        }
    }
}
