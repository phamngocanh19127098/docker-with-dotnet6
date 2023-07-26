namespace DataAccess.DTO.Response;

public class OrderResponse
{
    public int Id { get; set; }
    public double? TotalPrice { get; set; }
    public int? Quantity { get; set; }
    public int? PassengerId { get; set; }

}