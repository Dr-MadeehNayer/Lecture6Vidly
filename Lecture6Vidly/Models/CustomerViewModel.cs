using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lecture6Vidly.Models
{
    public class CustomerViewModel
    {
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
        public Customer Customer { get; set; }
    }
}