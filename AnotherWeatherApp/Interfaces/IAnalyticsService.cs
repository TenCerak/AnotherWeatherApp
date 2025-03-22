using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AnotherWeatherApp.Interfaces
{
    public interface IAnalyticsService
    {
        bool Configured { get; }

        void Track(string trackIdentifier);

        void Track(string trackIdentifier, IDictionary<string, string>? table);

        void Track(string trackIdentifier, string key, string value);

        void Report(Exception exception,
                           string key,
                           string value,
                           [CallerMemberName] string callerMemberName = "",
                           [CallerLineNumber] int lineNumber = 0,
                           [CallerFilePath] string filePath = "");

        void Report(Exception exception,
                                  IDictionary<string, string>? properties = null,
                                  [CallerMemberName] string callerMemberName = "",
                                  [CallerLineNumber] int lineNumber = 0,
                                  [CallerFilePath] string filePath = "");
    }
}
