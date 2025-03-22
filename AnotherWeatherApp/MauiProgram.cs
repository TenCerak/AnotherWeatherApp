using Microsoft.Extensions.Logging;
using Sentry.Maui;
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
using System.Diagnostics;

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
                ConfigureSentryOptions(options);
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
    static void ConfigureSentryOptions(SentryMauiOptions options)
    {
        options.TracesSampleRate = 1.0;
        options.IncludeTextInBreadcrumbs = true;
        options.IncludeTitleInBreadcrumbs = true;
        options.IncludeBackgroundingStateInBreadcrumbs = true;
        options.StackTraceMode = StackTraceMode.Enhanced;
        options.IsGlobalModeEnabled = true;

        options.TracesSampleRate = 1.0;
        options.ProfilesSampleRate = 1.0;


        ConfigureDebugSentryOptions(options);
        ConfigureReleaseSentryOptions(options);

        [Conditional("DEBUG")]
        static void ConfigureDebugSentryOptions(SentryMauiOptions options)
        {
            options.Dsn = "https://b342aeab536f6de9ea0cc578e4c78dc1@o4508921182617600.ingest.de.sentry.io/4508921184911440";
            options.Debug = true;
            options.Environment = "DEBUG";
            options.DiagnosticLevel = SentryLevel.Debug;
        }

        [Conditional("RELEASE")]
        static void ConfigureReleaseSentryOptions(SentryMauiOptions options)
        {
            options.Dsn = "https://b342aeab536f6de9ea0cc578e4c78dc1@o4508921182617600.ingest.de.sentry.io/4508921184911440";
            options.Environment = "RELEASE";
        }
    }
}
