using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Web.Hosting;
using FinanceFate.Areas.FinancialData;
namespace FinanceFate.Areas.Game
{
    public class Game
    {
        public int GameId;
        public int GameType;
        public int Score;
        public string PrevPerson; 
        public string CurrPerson;
        public int CurrCat;
        public static void CreateGame(int GameID)
        {


            using System.Data.SQLite.SQLiteConnection connection = new SQLiteConnection("Data Source= " + HostingEnvironment.MapPath("/App_Data/Main.db"));
            connection.Open();

            using (SQLiteCommand command = new SQLiteCommand("INSERT INTO Games (GameID, GameType,Score,PrevPerson,CurrPerson,CurrCat) VALUES (@0,@1,@2,@3,@4,@5)", connection))
            {

                command.Parameters.AddWithValue("@0", GameID);
                command.Parameters.AddWithValue("@1", 0);
                command.Parameters.AddWithValue("@2", 0);
                command.Parameters.AddWithValue("@3", GetData.getRandomPerson().AccountID);
                command.Parameters.AddWithValue("@4", "");
                command.Parameters.AddWithValue("@5", "");

                command.ExecuteNonQuery();
            }

        }
        public static Game getGameFromGameId(int Id)
        {
            using System.Data.SQLite.SQLiteConnection connection = new SQLiteConnection("Data Source= " + HostingEnvironment.MapPath("/App_Data/Main.db"));
            connection.Open();

            using (SQLiteCommand command = new SQLiteCommand("SELECT* FROM Games WHERE GameID=@0", connection))
            {
                command.Parameters.AddWithValue("@0", Id);



                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    //loops through all rows of data returned, as the reader moves down the database


                    while (reader.Read())
                    {
                        Game g = new Game();
                        //using reader.GetString() to get the string in the colunm n from the left
                        //adding recieved string to data holder
                        g.GameId = reader.GetInt32(0);
                        g.GameType = reader.GetInt32(1);
                        g.Score = reader.GetInt32(2);
                        g.PrevPerson = reader.GetString(3);
                        g.CurrPerson = reader.GetString(4);
                        g.CurrCat = reader.GetInt32(5);
                        return g;
                    }
                }
                return null;

            }

        }
        public static Game SetPrevGame(int GID, string PID)
        {
            using System.Data.SQLite.SQLiteConnection connection = new SQLiteConnection("Data Source= " + HostingEnvironment.MapPath("/App_Data/Main.db"));
            connection.Open();

            using (SQLiteCommand command = new SQLiteCommand("UPDATE Games SET PrevPerson = @0 WHERE GameID = @1", connection))
            {
                command.Parameters.AddWithValue("@0", PID);
                command.Parameters.AddWithValue("@1", GID);


                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    //loops through all rows of data returned, as the reader moves down the database


                    while (reader.Read())
                    {
                        Game g = new Game();
                        //using reader.GetString() to get the string in the colunm n from the left
                        //adding recieved string to data holder
                        g.GameId = reader.GetInt32(0);
                        g.GameType = reader.GetInt32(1);
                        g.Score = reader.GetInt32(2);
                        g.PrevPerson = reader.GetString(3);
                        g.CurrPerson = reader.GetString(4);

                        return g;
                    }
                }
                return null;

            }
        }
        public static Game SetCurrGame(int GID, string PID)
        {
            using System.Data.SQLite.SQLiteConnection connection = new SQLiteConnection("Data Source= " + HostingEnvironment.MapPath("/App_Data/Main.db"));
            connection.Open();

            using (SQLiteCommand command = new SQLiteCommand("UPDATE Games SET CurrPerson = @0 WHERE GameID = @1", connection))
            {
                command.Parameters.AddWithValue("@0", PID);
                command.Parameters.AddWithValue("@1", GID);


                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    //loops through all rows of data returned, as the reader moves down the database


                    while (reader.Read())
                    {
                        Game g = new Game();
                        //using reader.GetString() to get the string in the colunm n from the left
                        //adding recieved string to data holder
                        g.GameId = reader.GetInt32(0);
                        g.GameType = reader.GetInt32(1);
                        g.Score = reader.GetInt32(2);
                        g.PrevPerson = reader.GetString(3);
                        g.CurrPerson = reader.GetString(4);

                        return g;
                    }
                }
                return null;

            }
        }
        public static void setScore(int GID, int Score)
        {
            using System.Data.SQLite.SQLiteConnection connection = new SQLiteConnection("Data Source= " + HostingEnvironment.MapPath("/App_Data/Main.db"));
            connection.Open();

            using (SQLiteCommand command = new SQLiteCommand("UPDATE Games SET Score = @0 WHERE GameID = @1", connection))
            {
                command.Parameters.AddWithValue("@0", Score);
                command.Parameters.AddWithValue("@1", GID);

                command.ExecuteNonQuery();
                

            }
        }
        public static void setGM(int GID, int GM)
        {
            using System.Data.SQLite.SQLiteConnection connection = new SQLiteConnection("Data Source= " + HostingEnvironment.MapPath("/App_Data/Main.db"));
            connection.Open();

            using (SQLiteCommand command = new SQLiteCommand("UPDATE Games SET CurrCat = @0 WHERE GameID = @1", connection))
            {
                command.Parameters.AddWithValue("@0", GM);
                command.Parameters.AddWithValue("@1", GID);

                command.ExecuteNonQuery();


            }
        }
    }
}

