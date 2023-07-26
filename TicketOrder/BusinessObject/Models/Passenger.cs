using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Passenger
    {
        public Passenger()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? IdentityCard { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
