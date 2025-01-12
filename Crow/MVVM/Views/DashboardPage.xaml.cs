using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;

namespace Crow.MVVM.Views;

public partial class DashboardPage : Microsoft.Maui.Controls.TabbedPage
{
    public DashboardPage()
    {
        InitializeComponent();

        // Set the tab bar placement to the bottom for Android
        this.On<Microsoft.Maui.Controls.PlatformConfiguration.Android>().SetToolbarPlacement(
            Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific.ToolbarPlacement.Bottom);
    }
}
