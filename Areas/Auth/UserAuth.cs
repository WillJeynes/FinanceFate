using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;
using System.Web.Hosting;
namespace FinanceFate.Auth
{
    public class UserAuth
    {
     
        public static bool CheckCombination(string Uname, string P)
        {
            //Checks if a username and password link to a user.
            using System.Data.SQLite.SQLiteConnection connection = new SQLiteConnection("Data Source= " + HostingEnvironment.MapPath("/App_Data/Main.db"));
            connection.Open();
            string databaseReturn = string.Empty;

            using (SQLiteCommand command = new SQLiteCommand("SELECT HashedUserPass FROM Auth where UserLogin=@0", connection))
            {
                command.Parameters.AddWithValue("@0", Uname);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        databaseReturn = reader.GetString(0);
                    }
                }
            }
            //Check if hashed user passwords match
            return (P == databaseReturn);
        }
        

    }
}