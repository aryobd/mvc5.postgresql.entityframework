using Npgsql;
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

where a.group_code = @group_code --> MENGGUNAKAN PARAMETER
and a.class_code = @class_code --> MENGGUNAKAN PARAMETER
";

            // DEFINE THE PARAMETERS
            NpgsqlParameter param1 = new NpgsqlParameter("@group_code", NpgsqlTypes.NpgsqlDbType.Smallint)
            {
                Value = (Int16)1 // ASSIGN A VALUE TO THE @group_code PARAMETER
            };

            NpgsqlParameter param2 = new NpgsqlParameter("@class_code", NpgsqlTypes.NpgsqlDbType.Varchar)
            {
                Value = "1A" // ASSIGN A VALUE TO THE @class_code PARAMETER
            };

            // EXECUTE THE QUERY WITH PARAMETERS AND MAP TO THE MODEL
            var qry = dbContext.Database.SqlQuery<CommTrClass>(sqlQuery, param1, param2).AsQueryable(); // WITH PARAMETERS

            // EXECUTE THE QUERY AND MAP TO THE MODEL
            //var qry = dbContext.Database.SqlQuery<CommTrClass>(sqlQuery).AsQueryable(); // WITHOUT PARAMETERS

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
