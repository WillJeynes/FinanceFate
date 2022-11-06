using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinanceFate.Areas.FinancialData;
using FinanceFate.Areas.Transactions;
namespace FinanceFate.Controllers
{
    public class DesignController : Controller
    {
        public ActionResult Index(string root, string page)
        {
            //return home view
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
