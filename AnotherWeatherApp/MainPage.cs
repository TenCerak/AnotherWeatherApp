namespace AnotherWeatherApp;

public class MainPage : ContentPage
{
	public MainPage()
	{
		BatchBegin();

		Content = new VerticalStackLayout
		{
			Children = {
				new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Text = "Welcome to .NET MAUI!"
				}
			}
		};

		BatchCommit();
	}
}