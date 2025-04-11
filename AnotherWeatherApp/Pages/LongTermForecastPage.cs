using AnotherWeatherApp.Interfaces;
using AnotherWeatherApp.Models;
using AnotherWeatherApp.Pages.Base;
using DevExpress.Maui.Controls;
using DevExpress.Maui.CollectionView;
using CommunityToolkit.Maui.Markup;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;
using DevExpress.Maui.Core;

namespace AnotherWeatherApp.ViewPages;

public class LongTermForecastPage : BaseContentPage<LongTermForecastViewModel>
{
    public LongTermForecastPage(LongTermForecastViewModel model, IAnalyticsService analyticsService) : base(model, analyticsService)
    {
        model.LoadDataAsync().ConfigureAwait(false);
        Title = Properties.Resources.LongTermForecast;
        Content = new StackLayout()
        {
            new DXCollectionView
            {
                IsPullToRefreshEnabled = true,
                PullToRefreshCommand = new Command(async () =>
                {
                    await model.LoadDataAsync().ConfigureAwait(true);
                }),
                ItemTemplate = new DataTemplate(() =>
                {
                    return new DXBorder()
                    {
                        Margin = new Thickness(10),
                        CornerRadius = 8,
                        BorderColor = Colors.DarkGray,
                        BorderThickness = 2,
                        Content = new Grid()
                        {
                            ColumnDefinitions = Columns.Define(Star, Star,Star),
                            RowDefinitions = Rows.Define(Auto, Auto,Auto,Auto,Auto,Auto),
                            Children =
                            {
                                new Image().Bind(Image.SourceProperty,nameof(DailyForecast.IconSource)).Row(0).RowSpan(6).Column(0).Size(100).CenterVertical().CenterHorizontal(),

                                new Label().Bind(Label.TextProperty, nameof(DailyForecast.Time),stringFormat: "{0:d.M.yyyy}").Padding(10).Row(0).Column(1),
                                new Label().Bind(Label.TextProperty, nameof(DailyForecast.Description)).Row(0).Column(2),
                                
                                new HorizontalStackLayout()
                                {
                                    new Image().AppThemeBinding(Image.SourceProperty,model.DaySourceLight,model.DaySourceDark).Center().Size(20).Margin(10),
                                    new HorizontalStackLayout()
                                    {
                                        new Label().Bind(Label.TextProperty, nameof(DailyForecast.TemperatureMorning),stringFormat: "{0:0}/"),
                                        new Label().Bind(Label.TextProperty, nameof(DailyForecast.Temperature),stringFormat: "{0:0}/"),
                                        new Label().Bind(Label.TextProperty, nameof(DailyForecast.TemperatureEvening),stringFormat: "{0:0}°C")
                                    }.Center()
                                }
                                .Row(2)
                                .Column(1),

                                new HorizontalStackLayout()
                                {
                                    new Image().AppThemeBinding(Image.SourceProperty,model.NightSourceLight,model.NightSourceDark).Center().Size(20).Margin(10),
                                    new Label().Bind(Label.TextProperty, nameof(DailyForecast.TemperatureNight),stringFormat: "{0:0.0}°C").TextCenter().Center()
                                }
                                .Row(2)
                                .Column(2),

                                new HorizontalStackLayout()
                                {
                                    new Image().AppThemeBinding(Image.SourceProperty,model.WindSourceLight,model.WindSourceDark).Center().Size(20).Margin(10),
                                    new Label().Bind(Label.TextProperty, nameof(DailyForecast.PrecipitationProbability),stringFormat: "{0:0.0}m/s").TextCenter().Center()
                                }
                                .Row(3)
                                .Column(1),

                                new HorizontalStackLayout()
                                {
                                    new Image().AppThemeBinding(Image.SourceProperty,model.HumiditySourceLight,model.HumiditySourceDark).Center().Size(20).Margin(10),
                                    new Label().Bind(Label.TextProperty, nameof(DailyForecast.Humidity),stringFormat: "{0:0}%").TextCenter().Center()
                                }
                                .Row(3)
                                .Column(2),

                                new HorizontalStackLayout()
                                {
                                    new Image().AppThemeBinding(Image.SourceProperty,model.RainSourceLight,model.RainSourceDark).Center().Size(20).Margin(10),
                                    new Label().Bind(Label.TextProperty, nameof(DailyForecast.RainPrecipitation),stringFormat: "{0:0.0}mm").TextCenter().Center()
                                }
                                .Row(4)
                                .Column(1),

                                new HorizontalStackLayout()
                                {
                                    new Image().AppThemeBinding(Image.SourceProperty,model.SnowSourceLight,model.SnowSourceDark).Center().Size(20).Margin(10),
                                    new Label().Bind(Label.TextProperty, nameof(DailyForecast.SnowPrecipitation),stringFormat: "{0:0.0}mm").TextCenter().Center()
                                }
                                .Row(4)
                                .Column(2),

                                new HorizontalStackLayout()
                                {
                                    new Image().AppThemeBinding(Image.SourceProperty,model.SunriseSourceLight,model.SunriseSourceDark).Center().Size(20).Margin(10),
                                    new Label().Bind(Label.TextProperty, nameof(DailyForecast.Sunrise),stringFormat: "{0:HH:mm}").TextCenter().Center()
                                }
                                .Row(5)
                                .Column(1),

                                new HorizontalStackLayout()
                                {
                                    new Image().AppThemeBinding(Image.SourceProperty, model.SunsetSourceLight, model.SunsetSourceDark).Center().Size(20).Margin(10),
                                    new Label().Bind(Label.TextProperty, nameof(DailyForecast.Sunset),stringFormat: "{0:HH:mm}").TextCenter().Center()
                                }
                                .Row(5)
                                .Column(2),

                            }

                        }

                    };

                }),

            }
            .Assign(out DXCollectionView view)
            .Bind(DXCollectionView.ItemsSourceProperty, static (LongTermForecastViewModel vm) => vm.DailyForecasts)
            .Bind(DXCollectionView.IsRefreshingProperty, static (LongTermForecastViewModel vm) => vm.IsLoading)

            };

    }
}