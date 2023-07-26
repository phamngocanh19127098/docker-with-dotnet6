namespace DataAccess.DTO.Request.Ticket;

public class UpdateTicketRequest
{
    public int TicketId { get; set; }
    public double? TotalPrice { get; set; }
    public int? Quantity { get; set; }
    public double? TotalTicket { get; set; }
    public string? Departure { get; set; }
    public string? Arrival { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public int? ClassId { get; set; }
    public int? OrderId { get; set; }

}