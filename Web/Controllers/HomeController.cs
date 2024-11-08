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
a.class_code,
a.class_desc,
a.meter_size_code,

b.group_desc

from comm.tr_class a

join comm.tr_group b
on a.group_code = b.group_code
";

            // EXECUTE THE QUERY AND MAP TO THE MODEL
            var qry = dbContext.Database.SqlQuery<CommTrClass>(sqlQuery).AsQueryable();
            List<CommTrClass> lst = qry.ToList();

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
