namespace WindowsGenericPopups.WPFCoreLibrary.Popups;
public class TimerWPFPopupClass : IOpenTimedPopup
{
    private TaskCompletionSource _tcs = new();
    private Window? _window;
    private bool _hasClosed = false;
    async Task IOpenTimedPopup.OpenPopupAsync(string message, int milliseconds, EnumTimePopupDisplayMode mode)
    {
        ToastWindow.ExtraText = $"Closes out in {milliseconds} milliseconds";
        _tcs = new();
        _hasClosed = false;
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
        // Start delay and close after time elapses
        _ = Task.Run(async () =>
        {
            await Task.Delay(milliseconds);
            HandleClose();
        });
        await _tcs.Task;
    }
    private void HandleClose()
    {
        if (_hasClosed)
        {
            return;
        }
        _hasClosed = true;
        if (_window != null)
        {
            _window.Dispatcher.Invoke(() =>
            {
                _window.Close();
            });
            _window = null;
        }
        _tcs.TrySetResult();
    }
}