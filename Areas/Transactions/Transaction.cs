using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Http.Headers;

namespace FinanceFate.Areas.Transactions
{
    public class Transaction
    {
        public double ammount;
        public string merchantName;
        public string message;
        public string description;
        public string category;
        static Random random = new Random();
        public static string authJWT = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJuYmYiOjE2NjI0MjI0MDAsImFwaV9zdWIiOiIyMTc5ZjZiZGM4YjQ4NzU3OGFlZTFjN2ZhYTYxMGQ2M2E2ZDUwMDdiZDI2YTNmNDM4ZTI2NzU4NTc0Y2YzNGQ0MTY3NTEyMzIwMDAwMCIsInBsYyI6IjVkY2VjNzRhZTk3NzAxMGUwM2FkNjQ5NSIsImV4cCI6MTY3NTEyMzIwMCwiZGV2ZWxvcGVyX2lkIjoiMjE3OWY2YmRjOGI0ODc1NzhhZWUxYzdmYWE2MTBkNjNhNmQ1MDA3YmQyNmEzZjQzOGUyNjc1ODU3NGNmMzRkNCJ9.XVLcG2X4PPMPnwrWlWFP0Iw6hvdYkV6KgzKcCapVXTsF-gq-7yNI-ChtzpKqWCvi7WFket9te8aGOwFAg1eJjWFJOTqqHumm_q_HwtaoUDVlxRJJ5kAoz6Kl88vff_YCSQ_JY-QDnlvBrsMUpkBAlrhtA-ecB2SDLPi45bcqmV37TS-eeJAIk0EC9ELHlKSFXVXNy7KmAELGzXxjstMprRCbRhQtEZ_P1VckSnG8fNhR_VS721OMGvYuDd5zEA_25hs_42PsxkEJzYZYxUd9BadBZAjxP0-SY_RY2zgSKc5CDu6B3XHmB4ICy5wg06DVUa1ymXrh6ua01Bc0KyCCYA";
        public static List<Transaction> GetAllForID(int id)
        {
            List<Transaction> transactionList = new List<Transaction>();
            using (HttpClient client = new HttpClient())
            {

                //setting URL
                client.BaseAddress = new Uri("https://sandbox.capitalone.co.uk/developer-services-platform-pr/api/data/transactions/accounts/"+ id + "/transactions");
                string urlParameters = string.Format("");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + authJWT);
                //client.DefaultRequestHeaders.Add("Content-Type", "application/json");
                client.DefaultRequestHeaders.Add("version", "1.0");

                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //requesting response
                HttpResponseMessage response = client.GetAsync(urlParameters).Result;
                if (response.IsSuccessStatusCode)
                {
                    //decoding data
                    string main = response.Content.ReadAsStringAsync().Result;

                    //return main;

                    dynamic json = System.Web.Helpers.Json.Decode(main);
                    //currentWeather = new WeatherPoint() { windspeed = json.hourly[0].wind_speed, pop = json.hourly[0].pop * 100, start = DateTime.Now };
                    foreach (dynamic dyn in json["Transactions"])
                    {
                       Transaction transaction = new Transaction();
                        transaction.ammount = (double)(dyn["Amount"]);
                        transaction.merchantName = dyn["Merchant"]["Name"];
                        transaction.description= dyn["Merchant"]["Description"];
                        transaction.message= dyn["Message"];
                        transaction.category = dyn["Merchant"]["Category"];
                        transactionList.Add(transaction);
                    }

                    
                }


            }
            return transactionList;
        }
    }
}
