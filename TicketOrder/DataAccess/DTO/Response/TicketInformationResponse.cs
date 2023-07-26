namespace DataAccess.DTO.Response;

public class TicketInformationResponse
{
    public int Id { get; set; }
    public int? TicketId { get; set; }
    public double? TotalPrice { get; set; }
    public int? Quantity { get; set; }
    public double? TotalTicket { get; set; }
    public string? Departure { get; set; }
    public string? Arrival { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public int? ClassId { get; set; }
}