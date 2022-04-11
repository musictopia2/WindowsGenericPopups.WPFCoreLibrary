namespace WindowsGenericPopups.WPFCoreLibrary.Windows;
public static class SimpleStartUp
{
    private readonly static ServiceCollection _services = new();
    private static ServiceProvider? _provides;
    public static Action<ServiceCollection>? ExtraServiceProcesses { get; set; } //this was a good idea.
    //one extra service for sure that is needed is who is going to handle when the popup closes.
    private static bool _loaded = false;
    public static ServiceProvider GetProvider()
    {
        if (_loaded)
        {
            return _provides!;
        }
        _services.AddBlazorWebView();
        _services.RegisterWPFServices();
        _services.RegisterBlazorBeginningClasses(); //forgot this too.
        _services.AddSingleton<IOpenSimplePopup, SimpleWPFPopupClass>(); //i think.
        ExtraServiceProcesses?.Invoke(_services);
        _loaded = true;
        _provides = _services.BuildServiceProvider();
        return _provides;
    }
}