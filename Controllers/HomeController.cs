using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinanceFate.Areas.FinancialData;
using FinanceFate.Areas.Transactions;
namespace FinanceFate.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string root, string page)
        {
            //return home view
            ViewBag.Title = "Home Page";

            if (root == "testing")
            {
                ViewBag.main = GetData.GetAccounts();
                return View("testing");
            }
            if (root == "testing2")
            {
                ViewBag.main = CurrencyConverter.GetRate(page);
                return View("testing");
            }
            if (root == "testing3")
            {
                ViewBag.main = Transaction.GetAllForID(int.Parse(page));
                return View("testing");
            }

            return View();
        }
    }
}
