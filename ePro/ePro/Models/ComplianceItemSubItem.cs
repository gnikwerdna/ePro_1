using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ePro.Models
{
    public class ComplianceItemSubItem
    {
        public ComplianceItems ComplianceItems { get; set; }
        [Key, Column(Order = 0)]
        public int ComplianceItemID { get; set; }


        [Key, Column(Order = 1)]
        public int SubItemTo { get; set; }
        public ComplianceItems SubItem { get; set; }

    }
}