namespace WindowsGenericPopups.WPFCoreLibrary.Windows;
public class ToastWindow : Window
{
    public static string ExtraText { get; set; } = "";
    public ToastWindow(string title, string message)
    {
        Title = title;
        Background = Brushes.Aqua;
        Topmost = true;
        ResizeMode = ResizeMode.NoResize;
        WindowStyle = WindowStyle.None;
        ShowInTaskbar = false;
        Left = 1500;
        Top = 940;
        Width = 400;
        Height = 120;
        TextBlock temps = new();
        temps.TextWrapping = TextWrapping.Wrap;
        temps.FontSize = 24;
        temps.Text = $"{message}  {ExtraText}";
        Content = temps;
        Show();
    }
}