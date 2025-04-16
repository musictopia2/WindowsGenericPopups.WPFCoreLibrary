namespace WindowsGenericPopups.WPFCoreLibrary.Popups;
public class ToastWPFPopupClass : IOpenToastKeyPopup
{
    private TaskCompletionSource _tcs = new();
    private ToastWindow? _window;
    private KeyboardHook? _keys;
    private EnumKey? _closeKey;
    private bool _hasClosed = false;
    async Task IOpenToastKeyPopup.OpenPopupAsync(EnumKey closeKey, string message)
    {
        _tcs = new();
        _hasClosed = false;
        _closeKey = closeKey;
        _keys ??= new KeyboardHook();
        _keys.KeyUp += Keys_KeyUp;
        ToastWindow.ExtraText = $"Hit {closeKey} to close out.";
        _window = new("Toast Message", message);
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