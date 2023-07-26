namespace DataAccess.DTO.Request.Passenger;

public class CreatePassengerRequest
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? IdentityCard { get; set; }

}