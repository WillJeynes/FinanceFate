using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Http.Headers;

namespace FinanceFate.Areas.FinancialData
{
    public class CurrencyConverter
    {
        public static double GetRate(string symbol)
        {
            using (HttpClient client = new HttpClient())
            {

                //setting URL
                client.BaseAddress = new Uri("https://api.currencyfreaks.com/latest");
                string urlParameters = string.Format("?apikey=8c6b0708bd4645f2b8c9d03a5f8a6056&symbols=" + symbol);


                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //requesting response
                HttpResponseMessage response = client.GetAsync(urlParameters).Result;
                if (response.IsSuccessStatusCode)
                {
                    //decoding data
                    string main = response.Content.ReadAsStringAsync().Result;



                    dynamic json = System.Web.Helpers.Json.Decode(main);
                    //currentWeather = new WeatherPoint() { windspeed = json.hourly[0].wind_speed, pop = json.hourly[0].pop * 100, start = DateTime.Now };
                    foreach (dynamic dyn in json["Rates"])
                    {
                        return double.Parse(dyn.Value);
                    }
                }
            }
            return 0;
        }

    }
}
