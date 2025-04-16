namespace WindowsGenericPopups.WPFCoreLibrary.Popups;
public class ButtonWPFPopupClass : IOpenHybridPopup
{
    private TaskCompletionSource _tcs = new();
    private ButtonPopupWindow? _window;
    private KeyboardHook? _keys;
    private EnumKey? _closeKey;
    private bool _hasClosed = false;
    async Task IOpenHybridPopup.OpenPopupAsync(EnumKey closeKey, string message)
    {
        await OpenPopupInternalAsync(message, closeKey);
    }
    async Task IOpenButtonPopup.OpenPopupAsync(string message)
    {
        await OpenPopupInternalAsync(message, null);
    }
    private async Task OpenPopupInternalAsync(string message, EnumKey? optionalKey)
    {
        _tcs = new();
        _hasClosed = false;
        _closeKey = optionalKey;
        if (optionalKey.HasValue)
        {
            _keys ??= new KeyboardHook();
            _keys.KeyUp += Keys_KeyUp;
        }
        ButtonClosePopupComponent.Message = message;
        ButtonClosePopupComponent.OnButtonClicked = HandleClose;
        _window = new ButtonPopupWindow();
        _window.Show();
        await _tcs.Task;
    }
    private void Keys_KeyUp(EnumKey key)
    {
        if (_hasClosed || !_closeKey.HasValue)
        {
            return;
        }
        if (key == _closeKey.Value)
        {
            HandleClose();
        }
    }
    private void HandleClose()
    {
        if (_hasClosed)
        {
            return;
        }
        _hasClosed = true;
        if (_keys is not null)
        {
            _keys.KeyUp -= Keys_KeyUp;
            _keys.Dispose();
            _keys = null;
        }
        _window?.Close();
        _window = null;
        _tcs.TrySetResult();
    }
}