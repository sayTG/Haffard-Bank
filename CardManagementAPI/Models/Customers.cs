using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardManagementAPI.Models
{
    public class Customers
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Pin { get; set; }
        public bool Status { get; set; }
    }
}
