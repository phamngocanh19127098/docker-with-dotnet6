using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Order
    {
        public Order()
        {
            TicketOrders = new HashSet<TicketOrder>();
        }

        public int Id { get; set; }
        public double? TotalPrice { get; set; }
        public int? Quantity { get; set; }
        public int? PassengerId { get; set; }

        public virtual Passenger? Passenger { get; set; }
        public virtual ICollection<TicketOrder> TicketOrders { get; set; }
    }
}
