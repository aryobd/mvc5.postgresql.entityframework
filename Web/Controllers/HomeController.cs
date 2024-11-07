using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            PdamEntities dbContext = new PdamEntities();

            // USING NATIVE SQL - DEFINE THE SQL QUERY
            string sqlQuery = @"
                select
                a.group_code,
                a.group_desc
                
                from comm.tr_group a
                ";

            // EXECUTE THE QUERY AND MAP TO THE MODEL
            var qry = dbContext.Database.SqlQuery<CommTrGroup>(sqlQuery).AsQueryable();
            List<CommTrGroup> lst = qry.ToList();

            return View(lst);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
