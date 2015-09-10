using ePro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ePro.ViewModels
{
    public class SubItemsViewModel
    {
        public int ComplianceItemsID { get; set; }
        public int SubItemTo { get; set; }
        public string ItemName { get; set; }
        public int Checked { get; set; }
    }
}