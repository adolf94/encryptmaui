
using ToDoMaui.DataServices;
using ToDoMaui.Pages;
using Brushtail.FontAwesome.Mobile;

namespace ToDoMaui;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseFontAwesome()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("font-awesome-6-solid.ttf", "FontAwesomeSolid");
					fonts.AddFont("materialdesignicons-webfont.ttf", "MaterialFont");
			});
				
				builder.Services.AddSingleton<IRestDataServices, RestDataService>();
				builder.Services.AddSingleton<MainPage>();
				builder.Services.AddTransient<ManageToDoPage>();
				return builder.Build();
	}
}
