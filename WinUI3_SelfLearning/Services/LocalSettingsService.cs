using Microsoft.Extensions.Options;

using Windows.ApplicationModel;
using Windows.Storage;

using WinUI3_SelfLearning.Contracts.Services;
using WinUI3_SelfLearning.Core.Contracts.Services;
using WinUI3_SelfLearning.Core.Helpers;
using WinUI3_SelfLearning.Helpers;
using WinUI3_SelfLearning.Models;

namespace WinUI3_SelfLearning.Services;

/// <summary>
/// 目前这个类只负责从C:\Users\jinge\AppData\Local文件夹中加载和读写配置文件。
/// 如果想要从其他地方读写配置文件，则需要新建一个类，继承ILocalSettingsService接口，并添加到容器中。
/// </summary>
public class LocalSettingsService : ILocalSettingsService
{
    //如果在项目中的appsettings.json文件中配置了文件夹位置和文件名称，则优先从那里获取。如果没有配置，则使用此处的值作为默认值。
    private const string _defaultApplicationDataFolder = "WinUI3_SelfLearning/ApplicationData";
    private const string _defaultLocalSettingsFile = "LocalSettings.json";

    private readonly IFileService _fileService;
    private readonly LocalSettingsOptions _options;

    //本地应用的储存位置：C:\Users\jinge\AppData\Local
    private readonly string _localApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    //应用程序数据所在文件夹：C:\Users\jinge\AppData\Local\WinUI3_SelfLearning/ApplicationData
    private readonly string _applicationDataFolder;
    //读取的配置文件的名称：LocalSettings.json
    private readonly string _localsettingsFile;
    //把配置文件的信息读取之后，存在字典中
    private IDictionary<string, object> _settings;

    private bool _isInitialized;

    public LocalSettingsService(IFileService fileService, IOptions<LocalSettingsOptions> options)
    {
        _fileService = fileService;
        _options = options.Value;

        //得到的地址：C:\Users\jinge\AppData\Local\WinUI3_SelfLearning/ApplicationData
        _applicationDataFolder = Path.Combine(_localApplicationData, _options.ApplicationDataFolder ?? _defaultApplicationDataFolder);
        //得到的值：LocalSettings.json
        _localsettingsFile = _options.LocalSettingsFile ?? _defaultLocalSettingsFile;

        _settings = new Dictionary<string, object>();
    }

    private async Task InitializeAsync()
    {
        if (!_isInitialized)
        {
            _settings = await Task.Run(() => _fileService.Read<IDictionary<string, object>>(_applicationDataFolder, _localsettingsFile)) ?? new Dictionary<string, object>();

            _isInitialized = true;
        }
    }

    public async Task<T?> ReadSettingAsync<T>(string key)
    {
        if (RuntimeHelper.IsMSIX)
        {
            if (ApplicationData.Current.LocalSettings.Values.TryGetValue(key, out var obj))
            {
                return await Json.ToObjectAsync<T>((string)obj);
            }
        }
        else
        {
            await InitializeAsync();

            if (_settings != null && _settings.TryGetValue(key, out var obj))
            {
                return await Json.ToObjectAsync<T>((string)obj);
            }
        }

        return default;
    }

    public async Task SaveSettingAsync<T>(string key, T value)
    {
        if (RuntimeHelper.IsMSIX)
        {
            ApplicationData.Current.LocalSettings.Values[key] = await Json.StringifyAsync(value);
        }
        else
        {
            await InitializeAsync();

            _settings[key] = await Json.StringifyAsync(value);
            
            //把Setting写入到文件中。
            await Task.Run(() => _fileService.Save(_applicationDataFolder, _localsettingsFile, _settings));
        }
    }
}
