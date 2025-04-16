namespace WindowsGenericPopups.WPFCoreLibrary.Popups;
public class ControlledPopupInstance(Window window) : IControlledPopupInstance
{
    void IControlledPopupInstance.Close()
    {
        // Close the window manually
        window.Dispatcher.Invoke(() =>
        {
            window.Close();
        });
    }
}