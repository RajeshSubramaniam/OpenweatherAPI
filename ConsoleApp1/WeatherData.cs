using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class WeatherData
    {
    
        private List<string> hightemperaturedate = new List<string>();
        private List<string> sunnydate = new List<string>();
      
        public List<string> HighTemperatureDate 
        {
            get { return hightemperaturedate; }
            set { hightemperaturedate = value; }
        }

        public List<string> SunnyDate
        {
            get { return sunnydate; }
            set { sunnydate = value; }
        }
    }
}
