namespace DataAccess.DTO.Request.Order;

public class UpdateOrderRequest
{
    public int Id { get; set; }
    public double? TotalPrice { get; set; }
    public int? Quantity { get; set; }
    public int? PassengerId { get; set; }

}