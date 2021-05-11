using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp1
{
    public static class Report
    {
        public static void ResultLogger(WeatherData data)
        {
            Utility utilityObj = new Utility();
            string fileName = "WeatherReport_"+DateTime.Now.ToString("dd_MM_yyyy HH_mm_ss");
            string path = utilityObj.ExtractProjectPath() + "\\Reports\\"+fileName+".txt";
            StringBuilder HighTemp = new StringBuilder();
            StringBuilder SunnyDay = new StringBuilder();
            string finalOutput = string.Empty;

            foreach (var x in data.HighTemperatureDate)
                HighTemp.AppendLine(x.ToString());
            foreach (var x in data.SunnyDate)
                SunnyDay.AppendLine(x.ToString());

            if (HighTemp.Length == 0) 
                HighTemp.Append ("No Days, Looks like you have cold days coming!!!");
            if (SunnyDay.Length == 0)
                SunnyDay.Append("No Days, It won't be sunny out there!!");

            finalOutput = "\nUpcoming days that are above 20 degree:\n" + HighTemp +"=============" +
                           "\nUpcoming days that are described as Sunny:\n"+ SunnyDay + "\n=============";
            File.WriteAllText(path, finalOutput);
           
        }
    }
}
