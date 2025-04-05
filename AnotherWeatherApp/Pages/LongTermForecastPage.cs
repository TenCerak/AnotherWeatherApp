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
        Title = "Long term forecast";
        Content = new StackLayout()
        {
            new Button()
            {
                Text = "Reload",
                Command = new Command(async () =>
                {
                    await model.LoadDataAsync().ConfigureAwait(true);
                })
            },
            new DXCollectionView
            {
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
                            RowDefinitions = Rows.Define(Auto, Auto,Auto,Auto),
                            Children =
                            {
                                new Image().Bind(Image.SourceProperty,nameof(DailyForecast.IconSource)).Row(0).RowSpan(3).Column(0).Size(100).Center(),

                                new Label().Bind(Label.TextProperty, nameof(DailyForecast.LocationName)).Row(0).Column(1).ColumnSpan(2),
                                new Label().Bind(Label.TextProperty, nameof(DailyForecast.Time),stringFormat: "{0:d.M.yyyy}").Row(1).Column(1).ColumnSpan(2),
                                new HorizontalStackLayout()
                                {
                                    new Image().Bind(Image.SourceProperty, static (LongTermForecastViewModel vm) => vm.SunriseSource).BackgroundColor(Colors.White).Size(20),
                                    new Label().Bind(Label.TextProperty, nameof(DailyForecast.Sunrise),stringFormat: "{0:HH:mm}")
                                }
                                .Row(2).Column(1),
                                new Label().Bind(Label.TextProperty, nameof(DailyForecast.Sunset),stringFormat: "{0:HH:mm}").Row(2).Column(2),

                                new Label().Bind(Label.TextProperty, nameof(DailyForecast.Description)).Row(3).Column(1).ColumnSpan(2),
                            }

                        }

                    };

                }),

            }
            .Assign(out DXCollectionView view)
            .Bind(DXCollectionView.ItemsSourceProperty, static (LongTermForecastViewModel vm) => vm.DailyForecasts)

            };

    }
}