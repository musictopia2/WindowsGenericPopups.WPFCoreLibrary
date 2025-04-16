namespace WindowsGenericPopups.WPFCoreLibrary.Popups;
public class SimpleWPFPopupClass(IAfterCloseSimplePopup afterClose) : IOpenSimplePopup
{
    private KeyboardHook? _keys;
    private EnumKey _closeKey;
    private SimplePopupWindow? _window;
    private bool _needsKey;
    void IOpenSimplePopup.OpenPopup(EnumKey closeKey, string message)
    {
        _closeKey = closeKey;
        if (_keys is null)
        {
            _keys = new();
            _keys.KeyUp += Keys_KeyUp;
        }
        _needsKey = true;
        SimplePopupComponent.Message = message;
        //do the work required for this.
        _window = new();
        _window.Show();
    }
    private void Keys_KeyUp(EnumKey key)
    {
        if (_window is null || _needsKey == false)
        {
            return;
        }
        if (key == _closeKey)
        {
            //this means you entered the key that is supposed to close it.
            _window.Close();
            _window = null;
            if (_keys is not null)
            {
                _keys.KeyUp -= Keys_KeyUp;
                _keys.Dispose();
                _keys = null;
            }
            _needsKey = false;
            afterClose.FinishProcess();
            return;
        }
    }
    
}