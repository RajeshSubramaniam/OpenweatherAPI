using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Net;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace ConsoleApp1
{
    public class Utility
    {
        /// <summary>
        /// Fetch test data
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> FetchData()
        {
            Dictionary<string, string> dataDictionary = new Dictionary<string, string>();
            XmlDocument docToRead = new XmlDocument();
            string fileToLoad = ExtractProjectPath()+ "\\Data.xml";
            docToRead.Load(fileToLoad);

            XmlNode node = docToRead.DocumentElement.SelectSingleNode("/root");
            foreach (XmlNode n in docToRead.DocumentElement.ChildNodes)
            {
                switch (n.Name)
                {
                    case "CityLat":
                        dataDictionary.Add("CityLat", n.InnerText);
                        break;
                    case "CityLong":
                        dataDictionary.Add("CityLong", n.InnerText);
                        break;
                    case "Exclude":
                        dataDictionary.Add("Exclude", n.InnerText);
                        break;
                    case "AuthKey":
                        dataDictionary.Add("AuthKey", n.InnerText);
                        break;
                    case "BaseUri":
                        dataDictionary.Add("BaseUri", n.InnerText);
                        break;
                }
            }
            return dataDictionary;
        }
        public string ExtractProjectPath()
        {
            string wokringDirecory = Environment.CurrentDirectory;
            string projectDictionary = Directory.GetParent(wokringDirecory).Parent.FullName;
            projectDictionary = projectDictionary.Replace(@"\bin", "");
            return projectDictionary;
        }
        /// <summary>
        /// Fetching required testdata from data.xml and building the API Uri
        /// </summary>
        /// <returns></returns>
        public string BuildAPiUri()
        {
            Dictionary<string, string> testData = FetchData();
            string outputUri = string.Empty;
            outputUri = String.Format(testData["BaseUri"], testData["CityLat"], testData["CityLong"], testData["Exclude"], testData["AuthKey"]);
            return outputUri;
        }
        /// <summary>
        /// Send API call to openweathermap.org/
        /// </summary>
        /// <param name="Uri"></param>
        /// <returns></returns>
        public dynamic GetResponseViaAPICall(string Uri)
        {
            var request = (HttpWebRequest)WebRequest.Create(Uri);
            request.Method = "GET";
            var response = (HttpWebResponse)request.GetResponse();
            string responseString;
            using (var stream = response.GetResponseStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    responseString = reader.ReadToEnd();
                }
            }
            JObject responseData = JObject.Parse(responseString);
            return responseData;
        }
        /// <summary>
        /// This checks the required condition and returns the dates
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public WeatherData FilterAndCheckResponse(dynamic response)
        {

            WeatherData propertiesObj = new WeatherData();
            JToken listOfDays = null;

            foreach (JProperty x in (JToken)response)
            {
                if (x.Name == "daily")
                {
                    listOfDays = x.Value;
                    foreach (var item in listOfDays.Children())
                    {
                        var temp = item["temp"]["max"];
                        var status = item["weather"][0]["main"];
                        DateTime date = ConvertTimestampToDate(item["dt"].ToString().Replace("{", "").Replace("}", ""));

                        if (Convert.ToDouble(temp) > 20)
                            propertiesObj.HighTemperatureDate.Add(date.ToString("dd/MM/yyyy"));
                        if (status .ToString()== "Sunny")
                            propertiesObj.SunnyDate.Add(date.ToString("dd/MM/yyyy"));

                    }
                    break;
                }

            }

            return propertiesObj;

        }

        /// <summary>
        /// API Respone returns timestamp, thus this method to change it to DateTime
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static DateTime ConvertTimestampToDate(string input)
        {
            double timestamp = Convert.ToDouble(input);
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }
    }
}
