using AnotherWeatherApp.Interfaces;
using AnotherWeatherApp.Pages.Base;

namespace AnotherWeatherApp.ViewPages;

public class MainPage : BaseContentPage<MainPageViewModel>
{
	public MainPage(MainPageViewModel model, IAnalyticsService analyticsService) : base(model, analyticsService)
    {
 
		Content = new VerticalStackLayout
		{

			Children = {
				new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Text = "Welcome to .NET MAUI!"
				},
				new Label {Text = "Funguje to?"},
				new Button {Text = "Sentry test", Command = new Command( () => { SentrySdk.CaptureMessage("Hello sentry"); }) }
            }
		};

	}
}  