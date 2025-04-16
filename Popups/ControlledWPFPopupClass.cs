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

        _window.Show();

        // Return an instance of a controlled popup so it can be closed later.
        _popupInstance = new ControlledPopupInstance(_window);
        return _popupInstance;
    }
}