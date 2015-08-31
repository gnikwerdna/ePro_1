using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ePro.Models
{
    public class ComplianceItems
    {

        [Key]
        public int ComplianceItemsID { get; set; }
        public string ItemName { get; set; }
        public bool EndItem { get; set; }
        public int? SubItemTo { get; set; }
        [ForeignKey("SubItemTo")]
        public ICollection<ComplianceItemSubItem> ItemToSubItem { get; set; }
        public ICollection<ComplianceItemSubItem> SubItemToItem { get; set; }
       // public virtual ICollection<ComplianceItems> SubIitems { get; set; }
        public virtual ICollection<Compliance> Compliance { get; set; }
       // public virtual ICollection<ComplianceItems> ComplianceItem { get; set; }
       
        
    }
}