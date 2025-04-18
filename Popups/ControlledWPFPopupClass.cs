namespace WindowsGenericPopups.WPFCoreLibrary.Popups;
public class ControlledWPFPopupClass : IOpenControlledPopup
{
    private Window? _window;
    private IControlledPopupInstance? _popupInstance;
    IControlledPopupInstance IOpenControlledPopup.OpenPopup(string message, EnumTimePopupDisplayMode mode)
    {
        if (mode == EnumTimePopupDisplayMode.Toast)
        {
            _window = new ToastWindow("Toast Message", message);
        }
        else
        {
            SimplePopupComponent.Message = message;
            _window = new SimplePopupWindow();
        }

        _window.ShowActivated = true;
        _window.Show();

        // Force the dispatcher to process UI rendering
        _window.Dispatcher.Invoke(() => { }, System.Windows.Threading.DispatcherPriority.ApplicationIdle);

        _popupInstance = new ControlledPopupInstance(_window);
        return _popupInstance;
    }
}