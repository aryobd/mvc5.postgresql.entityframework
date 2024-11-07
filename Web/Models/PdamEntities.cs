using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class PdamEntities : DbContext
    {
        public PdamEntities() : base("pdam_Entities")
        {
        }

        public virtual DbSet<CommTrGroup> CommTrGroup { get; set; }
        public virtual DbSet<CommTrClass> CommTrClass { get; set; }
    }

}
