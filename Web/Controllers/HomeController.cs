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
--and a.class_code = @class_code --> MENGGUNAKAN PARAMETER

order by a.group_code, a.class_code --> ORDER

--offset 20 limit 10 --> PAGING --> MULAI DARI BARIS 21 SEBANYAK 10 BARIS
offset @offset limit @limit --> PAGING --> MULAI DARI BARIS @offset + 1 SEBANYAK @limit BARIS
";

            // DEFINE THE PARAMETERS
            NpgsqlParameter param1 = new NpgsqlParameter("@group_code", NpgsqlTypes.NpgsqlDbType.Smallint)
            {
                Value = (Int16)1 // ASSIGN A VALUE TO THE @group_code PARAMETER
            };

            //NpgsqlParameter param2 = new NpgsqlParameter("@class_code", NpgsqlTypes.NpgsqlDbType.Varchar)
            //{
            //    Value = "1A" // ASSIGN A VALUE TO THE @class_code PARAMETER
            //};

            NpgsqlParameter param3 = new NpgsqlParameter("@offset", NpgsqlTypes.NpgsqlDbType.Integer)
            {
                Value = 0 // SET YOUR OFFSET VALUE (STARTING ROW, E.G., ROW 1)
            };

            NpgsqlParameter param4 = new NpgsqlParameter("@limit", NpgsqlTypes.NpgsqlDbType.Integer)
            {
                Value = 10 // SET YOUR LIMIT VALUE (NUMBER OF ROWS PER PAGE)
            };

            // EXECUTE THE QUERY WITH PARAMETERS AND MAP TO THE MODEL
            var qry = dbContext.Database.SqlQuery<CommTrClass>(sqlQuery, param1, /*param2,*/ param3, param4).AsQueryable(); // WITH PARAMETERS

            // EXECUTE THE QUERY AND MAP TO THE MODEL
            //var qry = dbContext.Database.SqlQuery<CommTrClass>(sqlQuery).AsQueryable(); // WITHOUT PARAMETERS

            List<CommTrClass> lst = qry.ToList();

            // ORDER BY WITH LINQ - QUERY SYNTAX
            var lst2 = from p in lst
                       orderby p.class_code descending
                       select p;

            // ORDER BY WITH LINQ - METHOD SYNTAX
            var lst3 = lst.OrderByDescending(p => p.class_code);

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
