namespace WindowsGenericPopups.WPFCoreLibrary.Popups;
public class SimpleWPFPopupClass(IAfterCloseSimplePopup afterClose) : IOpenSimplePopup
{
    private KeyboardHook? _keys;
    private EnumKey _closeKey;
    private SimplePopupWindow? _window;
    private bool _needsKey;
    private CancellationTokenRegistration _registration;
    private Action? _onPopupClosed;
    void IOpenSimplePopup.OpenPopup(EnumKey closeKey, string message, CancellationToken token, Action? onPopupClosed)
    {
        _closeKey = closeKey;
        _onPopupClosed = onPopupClosed;
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
        // Watch for cancellation
        if (token.CanBeCanceled)
        {
            _registration = token.Register(() =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ClosePopup(); // closes the popup if cancellation is requested
                });
            });
        }
    }
    private void CloseRegistration()
    {
        if (_registration.Token.CanBeCanceled)
        {
            _registration.Dispose();
        }
    }
    private void ClosePopup()
    {
        if (_window is null || _needsKey == false)
        {
            CloseRegistration();
            return;
        }
        _window.Close();
        _window = null;
        if (_keys is not null)
        {
            _keys.KeyUp -= Keys_KeyUp;
            _keys.Dispose();
            _keys = null;
        }
        _needsKey = false;
        CloseRegistration();
        _onPopupClosed?.Invoke();
        afterClose.FinishProcess();
        return;
    }
    private void Keys_KeyUp(EnumKey key)
    {
        if (key == _closeKey)
        {
            //this means you entered the key that is supposed to close it.
            ClosePopup();
        }
    }
}