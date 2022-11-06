using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinanceFate.Areas.Leaderboard;
    using FinanceFate.Areas.Leaderboard;
using FinanceFate.Areas.Game;
namespace FinanceFate.Controllers
{
    public class LeaderboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index(string root, string page, string user, string score)
        {
            if (user != null && root=="Register")
            {
                Position.add(user, int.Parse(score));
                return Redirect("/Leaderboard");
            }
            ViewBag.Leaderboard = Position.getLeaderboard();

            return View();

        }

    }
}