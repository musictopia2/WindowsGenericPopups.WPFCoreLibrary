namespace WindowsGenericPopups.WPFCoreLibrary.Popups;
public class SimpleWPFPopupClass : IOpenSimplePopup
{
    private KeyboardHook? _keys;
    public SimpleWPFPopupClass(IAfterCloseSimplePopup afterClose)
    {
        _afterClose = afterClose;
    }
    private EnumKey _closeKey;
    private readonly IAfterCloseSimplePopup _afterClose;
    private SimplePopupWindow? _window;
    void IOpenSimplePopup.OpenPopup(EnumKey closeKey, string message)
    {
        _closeKey = closeKey;
        _keys = new();
        _keys.KeyUp += Keys_KeyUp;
        SimplePopupComponent.Message = message;
        //do the work required for this.
        _window = new();
        _window.Show();
    }
    private void Keys_KeyUp(EnumKey key)
    {
        if (_window is null || _keys is null)
        {
            return;
        }
        if (key == _closeKey)
        {
            //this means you entered the key that is supposed to close it.
            _window.Close();
            _window = null;
            _keys.KeyUp -= Keys_KeyUp;
            _keys = null;
            _afterClose.FinishProcess();
            return;
        }
    }
}