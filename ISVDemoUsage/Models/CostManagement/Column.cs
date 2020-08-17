using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ISVDemoUsage.Models.CostManagement
{
    public class Column
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string type { get; set; } 
    }
}