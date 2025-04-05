using AnotherWeatherApp.Pages;
using AnotherWeatherApp.ViewPages;
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

            FlyoutBackdrop = Brush.Gray;
            FlyoutBackgroundColor = Colors.Gray;
        }

    }
}
