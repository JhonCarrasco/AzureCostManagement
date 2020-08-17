using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ISVDemoUsage.Models.CostManagement
{
    public class Property
    {
        [Key]
        public int id { get; set; }
        public string nextLink { get; set; }
        public List<Column> columns { get; set; }
        public List<Object[]> rows { get; set; }
    }
}