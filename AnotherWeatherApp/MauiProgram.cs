using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Maui;
using CommunityToolkit.Mvvm;
using AnotherWeatherApp.Interfaces;
using AnotherWeatherApp.Services;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using AnotherWeatherApp.ViewPages;
using AnotherWeatherApp.ViewModels.Base;
using AnotherWeatherApp.Pages.Base;

namespace AnotherWeatherApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
        var secretsFile = Path.Combine(FileSystem.AppDataDirectory, "secrets.json");
        string sentryDSN = string.Empty;
        if (File.Exists(secretsFile))
        {
            var json = File.ReadAllText(secretsFile);
            var secrets = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            
            if(secrets is not null)
                sentryDSN = secrets["SentryDSN"];
        }

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
                options.Dsn = sentryDSN;

                #if DEBUG
                options.Debug = true;
                #endif

                options.TracesSampleRate = 1.0;
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        RegisterServices(builder.Services);

        return builder.Build();
	}

    private static void RegisterServices(in IServiceCollection services)
    {
        services.AddSingleton<App>();
        services.AddSingleton<AppShell>();

        services.AddSingleton<IAnalyticsService, AnalyticsService>();

        services.AddTransientWithShellRoute<MainPage, MainPageViewModel>();
    }


    static IServiceCollection AddTransientWithShellRoute<TPage, TViewModel>(this IServiceCollection services) where TPage : BaseContentPage
    where TViewModel : BaseViewModel
    {
        return services.AddTransientWithShellRoute<TPage, TViewModel>(AppShell.GetPageRoute<TPage>());
    }
}
