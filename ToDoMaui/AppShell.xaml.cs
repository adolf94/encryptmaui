using ToDoMaui.Pages;

namespace ToDoMaui;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(ManageToDoPage), typeof(ManageToDoPage));
	}
}
