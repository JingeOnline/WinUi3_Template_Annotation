using CommunityToolkit.Mvvm.ComponentModel;

using WinUI3_SelfLearning.Contracts.ViewModels;
using WinUI3_SelfLearning.Core.Contracts.Services;
using WinUI3_SelfLearning.Core.Models;

namespace WinUI3_SelfLearning.ViewModels;

public partial class ContentGridDetailViewModel : ObservableRecipient, INavigationAware
{
    private readonly ISampleDataService _sampleDataService;

    [ObservableProperty]
    private SampleOrder? item;

    public ContentGridDetailViewModel(ISampleDataService sampleDataService)
    {
        _sampleDataService = sampleDataService;
    }

    public async void OnNavigatedTo(object parameter)
    {
        if (parameter is long orderID)
        {
            var data = await _sampleDataService.GetContentGridDataAsync();
            Item = data.First(i => i.OrderID == orderID);
        }
    }

    public void OnNavigatedFrom()
    {
    }
}
