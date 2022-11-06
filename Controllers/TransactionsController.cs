using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinanceFate.Areas.Leaderboard;
    using FinanceFate.Areas.Leaderboard;
using FinanceFate.Areas.Transactions;
using FinanceFate.Areas.Game;
namespace FinanceFate.Controllers
{
    public class TransactionsController : Controller
    {
        // GET: Dashboard
        public ActionResult Index(string root, string page, string acct)
        {
            ViewBag.acct = acct;
            ViewBag.Transactions = Transaction.GetAllForID(int.Parse(acct));

            return View();

        }

    }
}