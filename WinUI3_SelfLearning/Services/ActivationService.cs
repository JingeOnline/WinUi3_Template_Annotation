using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using WinUI3_SelfLearning.Activation;
using WinUI3_SelfLearning.Contracts.Services;
using WinUI3_SelfLearning.Views;

namespace WinUI3_SelfLearning.Services;

public class ActivationService : IActivationService
{
    private readonly ActivationHandler<LaunchActivatedEventArgs> _defaultHandler;
    private readonly IEnumerable<IActivationHandler> _activationHandlers;
    //用来初始化主题颜色
    private readonly IThemeSelectorService _themeSelectorService;
    
    //应用程序的外壳，UIElement是所有控件的基类。
    private UIElement? _shell = null;

    public ActivationService(ActivationHandler<LaunchActivatedEventArgs> defaultHandler, IEnumerable<IActivationHandler> activationHandlers, IThemeSelectorService themeSelectorService)
    {
        _defaultHandler = defaultHandler;
        _activationHandlers = activationHandlers;
        _themeSelectorService = themeSelectorService;
    }

    public async Task ActivateAsync(object activationArgs)
    {
        // Execute tasks before activation.
        await InitializeAsync();

        // Set the MainWindow Content.
        if (App.MainWindow.Content == null)
        {
            //在此处从容器中获得ShellPage的实例，并添加到MainWindow的Content中。
            _shell = App.GetService<ShellPage>();
            App.MainWindow.Content = _shell ?? new Frame();
            //new Frame()是一个空白的页面。
        }

        // Handle activation via ActivationHandlers.
        await HandleActivationAsync(activationArgs);

        // Activate the MainWindow.
        App.MainWindow.Activate();

        // Execute tasks after activation.
        await StartupAsync();
    }

    private async Task HandleActivationAsync(object activationArgs)
    {
        var activationHandler = _activationHandlers.FirstOrDefault(h => h.CanHandle(activationArgs));

        if (activationHandler != null)
        {
            await activationHandler.HandleAsync(activationArgs);
        }

        if (_defaultHandler.CanHandle(activationArgs))
        {
            await _defaultHandler.HandleAsync(activationArgs);
        }
    }

    private async Task InitializeAsync()
    {
        //这里的ConfigurAwait(false)是配置上下文。该种使用方式对UI线程更加友好。当异步方法被执行完毕后，返回的线程不再是主线程（UI线程），
        //让调用该异步方法的方法也能在其他线程被执行。

        //获取settings中用户保存的主题
        await _themeSelectorService.InitializeAsync().ConfigureAwait(false);
        await Task.CompletedTask;
    }

    private async Task StartupAsync()
    {
        //设置应用程序的主题色
        await _themeSelectorService.SetRequestedThemeAsync();
        await Task.CompletedTask;
    }
}
