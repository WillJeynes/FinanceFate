using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

    using FinanceFate.Areas.FinancialData;
using FinanceFate.Areas.Game;
namespace FinanceFate.Controllers
{
    public class GameController : Controller
    {
        // GET: Dashboard
        public ActionResult Index(string root, string page)
        {   
            //Return dashboard view to user
            //Make sure argument is correct before trying to return view to user
            if (root == "new")
            {
                //GetData.GetAccounts();    
                Random rnd = new Random();
                int GameID = rnd.Next(0, 100000);

                HttpCookie gameid = new HttpCookie("game");
                gameid.Values.Add("GID", GameID.ToString());
                gameid.Expires = DateTime.Now.AddDays(365);
                gameid.SameSite = SameSiteMode.Strict;
                gameid.Shareable = false;
                Response.Cookies.Add(gameid);


                Game.CreateGame(GameID);

                return Redirect("/game/play");
            }
            else if (root == "submithigh")
            {
                HttpCookie mcookie = Request.Cookies["game"];
                if (mcookie == null || mcookie.Values["GID"] == null)
                    return Redirect("/game/new");

                int GameID = int.Parse(mcookie.Values["GID"]);

                Game g = Game.getGameFromGameId(GameID);
                double done = 0;
                double dtwo = 0;
                int Mode = g.CurrCat;
                if (Mode == 0)
                {
                    done = double.Parse(GetData.getPersonFromPersonId(g.CurrPerson).Balance);
                    dtwo = double.Parse(GetData.getPersonFromPersonId(g.PrevPerson).Balance);
                }
                else if (Mode == 1)
                {
                    done = double.Parse(GetData.getPersonFromPersonId(g.CurrPerson).CreditScore);
                    dtwo = double.Parse(GetData.getPersonFromPersonId(g.PrevPerson).CreditScore);
                }
                else if (Mode == 2)
                {
                    done = double.Parse(GetData.getPersonFromPersonId(g.CurrPerson).RiskScore);
                    dtwo = double.Parse(GetData.getPersonFromPersonId(g.PrevPerson).RiskScore);
                }
                else if (Mode == 3)
                {
                    done = double.Parse(GetData.getPersonFromPersonId(g.CurrPerson).CreditLimit);
                    dtwo = double.Parse(GetData.getPersonFromPersonId(g.PrevPerson).CreditLimit);
                }

                if (done > dtwo)
                {
                    Game.SetPrevGame(GameID, Game.getGameFromGameId(GameID).CurrPerson);

                    Game.setScore(GameID, Game.getGameFromGameId(GameID).Score + 1);


                    return Redirect("/game/play");
                }
                else
                {
                    return Redirect("/Game/GameOver");
                }

                    
               
            }
            else if (root == "submitlow")
            {
                HttpCookie mcookie = Request.Cookies["game"];
                if (mcookie == null || mcookie.Values["GID"] == null)
                    return Redirect("/game/new");

                int GameID = int.Parse(mcookie.Values["GID"]);

                Game g = Game.getGameFromGameId(GameID);
                int Mode = g.CurrCat;
                double done = 0;
                double dtwo = 0;
                if (Mode == 0)
                {
                    done = double.Parse(GetData.getPersonFromPersonId(g.CurrPerson).Balance);
                    dtwo = double.Parse(GetData.getPersonFromPersonId(g.PrevPerson).Balance);
                }
                else if (Mode == 1)
                {
                    done = double.Parse(GetData.getPersonFromPersonId(g.CurrPerson).CreditScore);
                    dtwo = double.Parse(GetData.getPersonFromPersonId(g.PrevPerson).CreditScore);
                }
                else if (Mode == 2)
                {
                    done = double.Parse(GetData.getPersonFromPersonId(g.CurrPerson).RiskScore);
                    dtwo = double.Parse(GetData.getPersonFromPersonId(g.PrevPerson).RiskScore);
                }
                else if (Mode == 3)
                {
                    done = double.Parse(GetData.getPersonFromPersonId(g.CurrPerson).CreditLimit);
                    dtwo = double.Parse(GetData.getPersonFromPersonId(g.PrevPerson).CreditLimit);
                }

                if (done < dtwo)
                {
                    Game.SetPrevGame(GameID, Game.getGameFromGameId(GameID).CurrPerson);

                    Game.setScore(GameID, Game.getGameFromGameId(GameID).Score + 1);


                    return Redirect("/game/play");
                }
                else
                {
                    return Redirect("/Game/GameOver");
                }



            }
            else if (root == "play")
            {
                HttpCookie mcookie = Request.Cookies["game"];
                //if (mcookie == null || mcookie.Values["GID"] == null)
                //    return Redirect("/game/new");
                
                int GameID = int.Parse(mcookie.Values["GID"]);

                int Mode = (new Random()).Next(0, 4);

                //Game.CreateGame(GameID);
                Game.setGM(GameID, Mode);

                ViewBag.Score = Game.getGameFromGameId(GameID).Score;

                ViewBag.person1 = GetData.getPersonFromPersonId(Game.getGameFromGameId(GameID).PrevPerson);
                string[] modes = { "Balance", "Credit Score", "Risk Score", "Credit Limit" };
                ViewBag.Thing = modes[Mode];
                
                ViewBag.person2 = GetData.getRandomPerson();

                while (ViewBag.person1.AccountID == ViewBag.person2.AccountID)
                {
                    ViewBag.person2 = GetData.getRandomPerson();
                }

                
                Game.SetCurrGame(GameID, ViewBag.person2.AccountID);
                if (Mode == 0)
                {
                    ViewBag.person2.Balance = "***";
                }
                else if (Mode == 1)
                {
                    ViewBag.person2.CreditScore = "***";
                }
                else if (Mode == 2)
                {
                    ViewBag.person2.RiskScore = "**";
                }
                else if (Mode == 3)
                {
                    ViewBag.person2.CreditLimit = "***";
                }
                

                return View("Play");
            }
            else if (root == "GameOver")
            {
                HttpCookie mcookie = Request.Cookies["game"];
                if (mcookie == null || mcookie.Values["GID"] == null)
                    return Redirect("/game/new");

                int GameID = int.Parse(mcookie.Values["GID"]);


               
                @ViewBag.Score = Game.getGameFromGameId(GameID).Score.ToString();

                return View("GameOver");
            }


                ViewBag.GameID = 1;
            return View();
        }
    }
}