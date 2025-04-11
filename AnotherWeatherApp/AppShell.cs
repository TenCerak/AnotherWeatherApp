using AnotherWeatherApp.Pages;
using AnotherWeatherApp.ViewPages;
using CommunityToolkit.Maui.Markup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherWeatherApp
{
    partial class AppShell : Shell
    {
        public AppShell(MainPage page, DebugPage debugPage,DetailForecastPage detailForecastPage, LongTermForecastPage longTermForecastPage)
        {
            Items.Add(detailForecastPage);
            Items.Add(longTermForecastPage);
            Items.Add(page);
            Items.Add(debugPage);

            FlyoutHeader = new Image()
                .Source(ImageSource.FromFile("logo.svg"))
                .Aspect(Aspect.AspectFit)
                .Size(100)
                .CenterHorizontal()
                .Margins(10);

            FlyoutBackgroundColor = Colors.Gray;
            this.AppThemeColorBinding(AppShell.FlyoutBackgroundColorProperty, Colors.WhiteSmoke, Colors.Black);
        }

    }
}
