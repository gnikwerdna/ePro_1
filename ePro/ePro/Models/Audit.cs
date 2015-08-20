using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ePro.Models
{
    public class Audit
    {
        public Guid AuditID { get; set; }
        public string IPAddress { get; set; }
        public string UserName { get; set; }
        public string URLAccessed { get; set; }
        public DateTime TimeAccessed { get; set; }

        // Default Constructor
        public Audit() { }

    }
}