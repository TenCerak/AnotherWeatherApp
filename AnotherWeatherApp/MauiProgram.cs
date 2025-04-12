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
using AnotherWeatherApp.Pages;
using AnotherWeatherApp.Models;
using System.Reflection;
using DevExpress.Maui;
using AnotherWeatherApp.ViewModels;
using SkiaSharp.Views.Maui.Controls.Hosting;


namespace AnotherWeatherApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {

        var builder = MauiApp.CreateBuilder();


        var currentAssembly = Assembly.GetExecutingAssembly();

        var config = new ConfigurationBuilder()
            .AddJsonStream(currentAssembly.GetManifestResourceStream("AnotherWeatherApp.appsettings.json"))
            .Build();

        builder.Configuration.AddConfiguration(config);
        builder
            .UseDevExpress()
            .UseDevExpressControls()
            .UseDevExpressCharts()
            .UseDevExpressCollectionView()
            .UseMauiApp<App>()
            .UseSkiaSharp(true)
            .UseMauiCommunityToolkitMarkup()
            .UseMauiCommunityToolkit()

            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        
        builder.UseSentry(options =>
        {
            ConfigureSentryOptions(options, builder.Configuration);
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

        services.AddTransientWithShellRoute<MapPage, MapPageViewModel>();
        services.AddTransientWithShellRoute<DetailForecastPage, DetailForecastViewModel>();
        services.AddTransientWithShellRoute<LongTermForecastPage, LongTermForecastViewModel>();
        services.AddTransientWithShellRoute<LocationSettingsPage, LocationSettingsViewModel>();
        services.AddTransientWithShellRoute<DebugPage, DetailForecastViewModel>();


        services.AddSingleton<IWeatherService,OpenWeatherMapService>();
        services.AddSingleton<IGeocodingService,OpenWeatherMapService>();
    }


    static IServiceCollection AddTransientWithShellRoute<TPage, TViewModel>(this IServiceCollection services) where TPage : BaseContentPage
    where TViewModel : BaseViewModel
    {
        return services.AddTransientWithShellRoute<TPage, TViewModel>(AppShell.GetPageRoute<TPage>());
    }
    static void ConfigureSentryOptions(SentryMauiOptions options, IConfiguration configuration)
    {
        options.TracesSampleRate = 1.0;
        options.IncludeTextInBreadcrumbs = true;
        options.IncludeTitleInBreadcrumbs = true;
        options.IncludeBackgroundingStateInBreadcrumbs = true;
        options.StackTraceMode = StackTraceMode.Enhanced;
        options.IsGlobalModeEnabled = true;

        options.TracesSampleRate = 1.0;
        options.ProfilesSampleRate = 1.0;


        ConfigureDebugSentryOptions(options, configuration);
        ConfigureReleaseSentryOptions(options, configuration);

        [Conditional("DEBUG")]
        static void ConfigureDebugSentryOptions(SentryMauiOptions options, IConfiguration configuration)
        {
            options.Dsn = configuration["SentryDSN"];
            options.Debug = true;
            options.Environment = "DEBUG";
            options.DiagnosticLevel = SentryLevel.Debug;
        }

        [Conditional("RELEASE")]
        static void ConfigureReleaseSentryOptions(SentryMauiOptions options, IConfiguration configuration)
        {
            options.Dsn = configuration["SentryDSN"];
            options.Environment = "RELEASE";
        }
    }
}
