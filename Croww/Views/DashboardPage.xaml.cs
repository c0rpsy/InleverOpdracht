using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration;
using AndroidSpecific = Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;


namespace Croww.Views;
public partial class DashboardPage : TabbedPage
{
	public DashboardPage()
	{
		InitializeComponent();

        // Pins the dashboard page to the bottom of the page. 
        AndroidSpecific.TabbedPage.SetToolbarPlacement(this, AndroidSpecific.ToolbarPlacement.Bottom);

    }
}