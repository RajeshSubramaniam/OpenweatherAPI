using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Utility utilityObject = new Utility();
            
            //Building the URI From TestData
            string uri = utilityObject.BuildAPiUri();

            //Get API Response
            dynamic response = utilityObject.GetResponseViaAPICall(uri);

            //Filter days if Temperature>20 and/or Sunny
            WeatherData rawDates = utilityObject.FilterAndCheckResponse(response);

            //Generate report in .txt file
            Report.ResultLogger(rawDates);
        }
    }
}
