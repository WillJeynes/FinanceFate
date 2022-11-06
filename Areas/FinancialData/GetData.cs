using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Http.Headers;
using FinanceFate.Areas.Person;
using System.Data.SQLite;
using System.Web.Hosting;
namespace FinanceFate.Areas.FinancialData
{
    public class GetData
    {
        static Random random = new Random();
        public static string authJWT = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJuYmYiOjE2NjI0MjI0MDAsImFwaV9zdWIiOiIyMTc5ZjZiZGM4YjQ4NzU3OGFlZTFjN2ZhYTYxMGQ2M2E2ZDUwMDdiZDI2YTNmNDM4ZTI2NzU4NTc0Y2YzNGQ0MTY3NTEyMzIwMDAwMCIsInBsYyI6IjVkY2VjNzRhZTk3NzAxMGUwM2FkNjQ5NSIsImV4cCI6MTY3NTEyMzIwMCwiZGV2ZWxvcGVyX2lkIjoiMjE3OWY2YmRjOGI0ODc1NzhhZWUxYzdmYWE2MTBkNjNhNmQ1MDA3YmQyNmEzZjQzOGUyNjc1ODU3NGNmMzRkNCJ9.XVLcG2X4PPMPnwrWlWFP0Iw6hvdYkV6KgzKcCapVXTsF-gq-7yNI-ChtzpKqWCvi7WFket9te8aGOwFAg1eJjWFJOTqqHumm_q_HwtaoUDVlxRJJ5kAoz6Kl88vff_YCSQ_JY-QDnlvBrsMUpkBAlrhtA-ecB2SDLPi45bcqmV37TS-eeJAIk0EC9ELHlKSFXVXNy7KmAELGzXxjstMprRCbRhQtEZ_P1VckSnG8fNhR_VS721OMGvYuDd5zEA_25hs_42PsxkEJzYZYxUd9BadBZAjxP0-SY_RY2zgSKc5CDu6B3XHmB4ICy5wg06DVUa1ymXrh6ua01Bc0KyCCYA";
        public static string GetAccounts()
        {
            clearPersondDB();
            using (HttpClient client = new HttpClient())
            {

                //setting URL
                client.BaseAddress = new Uri("https://sandbox.capitalone.co.uk/developer-services-platform-pr/api/data/accounts");
                string urlParameters = string.Format("?state=eq:open");
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



                    dynamic json = System.Web.Helpers.Json.Decode(main);
                    //currentWeather = new WeatherPoint() { windspeed = json.hourly[0].wind_speed, pop = json.hourly[0].pop * 100, start = DateTime.Now };
                    foreach (dynamic dyn in json["Accounts"])
                    {
                        Person.Person p = new Person.Person();
                        p.Balance = String.Format("{0:0.00}",(double.Parse(dyn["balance"])/CurrencyConverter.GetRate(dyn["currencyCode"])));
                        p.CreditLimit = dyn["creditLimit"];
                        p.CreditScore = dyn["creditScore"];
                        p.FName = dyn["firstName"];
                        p.LName = dyn["lastName"];
                        string[] split = dyn["homeAddress"].Split(',');

                        p.Location = split[split.Length-2]  + ", " + split[split.Length - 1];
                        p.ProductType = dyn["productType"];
                        p.RiskScore = dyn["riskScore"];
                        p.AccountID = dyn["accountID"];
                        p.Currency = dyn["currencyCode"];
                        addPersontoDB(p);
                    }


                    //decimal WindSpeed = json.hourly[0].wind_speed;


                    //decimal ChanceOfRain = json.hourly[0].pop * 100;

                    return main;
                }


            }
            return "";
        }
        public static void clearPersondDB()
        {
            using System.Data.SQLite.SQLiteConnection connection = new SQLiteConnection("Data Source= " + HostingEnvironment.MapPath("/App_Data/Main.db"));
            connection.Open();
            using (SQLiteCommand command = new SQLiteCommand("DELETE FROM Persons", connection))
            {

                command.ExecuteNonQuery();
            }
        }
        public static void addPersontoDB(Person.Person p)
        {
            using System.Data.SQLite.SQLiteConnection connection = new SQLiteConnection("Data Source= " + HostingEnvironment.MapPath("/App_Data/Main.db"));
            connection.Open();

            using (SQLiteCommand command = new SQLiteCommand("INSERT INTO Persons (FName, LName, RiskScore, CreditScore, ProductType, Location, CreditLimit, Balance, PersonID, Currency) VALUES (@0,@1,@2,@3,@4,@5,@6,@7,@8,@9)", connection))
            {

                command.Parameters.AddWithValue("@0", p.FName);
                command.Parameters.AddWithValue("@1", p.LName);
                command.Parameters.AddWithValue("@2", p.RiskScore);
                command.Parameters.AddWithValue("@3", p.CreditScore);
                command.Parameters.AddWithValue("@4", p.ProductType);
                command.Parameters.AddWithValue("@5", p.Location);
                command.Parameters.AddWithValue("@6", p.CreditLimit);
                command.Parameters.AddWithValue("@7", p.Balance);
                command.Parameters.AddWithValue("@8", p.AccountID);
                command.Parameters.AddWithValue("@9", p.Currency);
                command.ExecuteNonQuery();
            }
        }
        public static Person.Person getRandomPerson()
        {
            using System.Data.SQLite.SQLiteConnection connection = new SQLiteConnection("Data Source= " + HostingEnvironment.MapPath("/App_Data/Main.db"));
            connection.Open();

            using (SQLiteCommand command = new SQLiteCommand("SELECT* FROM Persons ORDER BY RANDOM() LIMIT 1", connection))
            {




                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    //loops through all rows of data returned, as the reader moves down the database


                    while (reader.Read())
                    {
                        Person.Person p = new Person.Person();
                        //using reader.GetString() to get the string in the colunm n from the left
                        //adding recieved string to data holder
                        p.FName = reader.GetString(0);
                        p.LName = reader.GetString(1);
                        p.RiskScore = reader.GetString(2);
                        p.CreditScore = reader.GetString(3);
                        p.ProductType = reader.GetString(4);
                        p.Location = reader.GetString(5);
                        p.CreditLimit = reader.GetString(6);
                        p.Balance = reader.GetString(7);
                        p.AccountID = reader.GetString(8);
                        p.Currency = reader.GetString(9);

                        return p;
                    }
                }
                return null;

            }
        }
        public static Person.Person getPersonFromPersonId(string Id)
        {
            using System.Data.SQLite.SQLiteConnection connection = new SQLiteConnection("Data Source= " + HostingEnvironment.MapPath("/App_Data/Main.db"));
            connection.Open();

            using (SQLiteCommand command = new SQLiteCommand("SELECT* FROM Persons WHERE PersonID=@0", connection))
            {
                command.Parameters.AddWithValue("@0", Id);



                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    //loops through all rows of data returned, as the reader moves down the database


                    while (reader.Read())
                    {
                        Person.Person p = new Person.Person();
                        //using reader.GetString() to get the string in the colunm n from the left
                        //adding recieved string to data holder
                        p.FName = reader.GetString(0);
                        p.LName = reader.GetString(1);
                        p.RiskScore = reader.GetString(2);
                        p.CreditScore = reader.GetString(3);
                        p.ProductType = reader.GetString(4);
                        p.Location = reader.GetString(5);
                        p.CreditLimit = reader.GetString(6);
                        p.Balance = reader.GetString(7);
                        p.AccountID = reader.GetString(8);
                        p.Currency = reader.GetString(9);

                        return p;
                    }
                }
                return null;

            }
        }
    }
}