using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Class
    {
        public Class()
        {
            TicketInformations = new HashSet<TicketInformation>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public double? Price { get; set; }
        public double? Amount { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<TicketInformation> TicketInformations { get; set; }
    }
}
