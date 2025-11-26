using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Mare_Bogdan_Lab2_EB.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
