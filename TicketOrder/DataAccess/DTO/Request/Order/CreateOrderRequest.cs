namespace DataAccess.DTO.Request.Order;

public class CreateOrderRequest
{
    public double? TotalPrice { get; set; }
    public int? Quantity { get; set; }
    public int? PassengerId { get; set; }
    public int? TicketId { get; set; }

}