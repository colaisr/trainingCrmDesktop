using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training_CRM_Application
{
   public class Account
    {        
        public string Name { get; set; }
        public string Source { get; set; }
        public Status Status { get; set; }
        public string WebPage { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public bool Active { get; set; }
        public string CardNumber { get; set; }
        public DateTime CardExpires { get; set; }
        public string CCV { get; set; }
    }
}
