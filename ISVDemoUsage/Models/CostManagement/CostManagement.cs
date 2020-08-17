using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ISVDemoUsage.Models.CostManagement
{
    public class CostManagement
    {
        [Key]
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public Property properties { get; set; }
    }
}