
namespace WindowsGenericPopups.WPFCoreLibrary.Windows;
/// <summary>
/// Interaction logic for SimplePopupWindow.xaml
/// </summary>
public partial class SimplePopupWindow : Window
{
    public SimplePopupWindow()
    {
        Resources.Add("services", SimpleStartUp.GetProvider());
        InitializeComponent();
    }
}