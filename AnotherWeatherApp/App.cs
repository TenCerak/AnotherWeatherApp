using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherWeatherApp
{
    public partial class App : Application
    {
        protected override Window CreateWindow(IActivationState? activationState)
        {
            Windows[0].Page = new MainPage();
            return Windows[0];
        }
    }
}
