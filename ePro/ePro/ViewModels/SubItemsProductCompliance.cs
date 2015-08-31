using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ePro.ViewModels
{
    public class SubItemsProductCompliance
    {
        public int ComplianceItemsID { get; set; }
        public int SubItemTo { get; set; }
        public long ProductListingID { get; set; }
        public int Checked { get; set; }
    }
}