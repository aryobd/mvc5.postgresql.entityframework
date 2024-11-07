using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class CommTrGroup
    {
        [Key]
        public int group_code { get; set; }
        public string group_desc { get; set; }
    }
}
