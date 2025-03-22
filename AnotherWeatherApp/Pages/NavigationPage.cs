using AnotherWeatherApp.Interfaces;
using AnotherWeatherApp.Pages.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherWeatherApp.Pages
{
    public class NavPage : BaseContentPage
    {
        public NavPage(IAnalyticsService analytics) : base(analytics)
        {
            Title = "Navigation";
            Content = new VerticalStackLayout()
            {
                Children =
                {
                    new Label() { Text = "Navigation page"}
                }
            };
        }
    }
}
