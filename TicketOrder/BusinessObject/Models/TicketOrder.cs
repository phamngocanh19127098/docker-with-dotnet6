using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class TicketOrder
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public int? TicketId { get; set; }

        public virtual Order? Order { get; set; }
        public virtual TicketInformation? Ticket { get; set; }
    }
}
