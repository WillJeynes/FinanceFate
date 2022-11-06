using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceFate.Areas.Person;
using System.Data.SQLite;
using System.Web.Hosting;

namespace FinanceFate.Areas.Leaderboard
{
    public class Position
    {
        public string name;
        public int score;
        public static List<Position> getLeaderboard()
        {
            List<Position> p = new List<Position>();

            using System.Data.SQLite.SQLiteConnection connection = new SQLiteConnection("Data Source= " + HostingEnvironment.MapPath("/App_Data/Main.db"));
            connection.Open();

            using (SQLiteCommand command = new SQLiteCommand("SELECT * FROM Leaderboard ORDER BY score DESC LIMIT 10", connection))
            {




                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    //loops through all rows of data returned, as the reader moves down the database


                    while (reader.Read())
                    {
                        Position position = new Position();
                        position.name = reader.GetString(0);
                        position.score = reader.GetInt32(1);

                        p.Add(position);
                    }
                }
                

            }
            return p;
        }
        public static void add(string name, int score)
        {
           

            using System.Data.SQLite.SQLiteConnection connection = new SQLiteConnection("Data Source= " + HostingEnvironment.MapPath("/App_Data/Main.db"));
            connection.Open();

            using (SQLiteCommand command = new SQLiteCommand("INSERT INTO Leaderboard (name,score) VALUES (@0,@1)", connection))
            {

                command.Parameters.AddWithValue("@0", name);
                command.Parameters.AddWithValue("@1", score);


                command.ExecuteNonQuery();

            }
        }
    }
}
