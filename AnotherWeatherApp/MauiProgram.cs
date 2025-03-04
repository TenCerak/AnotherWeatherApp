using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Maui;
using CommunityToolkit.Mvvm;

namespace AnotherWeatherApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkitMarkup()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .UseSentry(options =>
            {
                options.Dsn = "https://b342aeab536f6de9ea0cc578e4c78dc1@o4508921182617600.ingest.de.sentry.io/4508921184911440";

                #if DEBUG
                options.Debug = true;
                #endif

                options.TracesSampleRate = 1.0;
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
