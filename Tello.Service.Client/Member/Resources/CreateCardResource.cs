namespace Tello.Service.Client.Member.Resources
{
    public record CreateCardResource
     (
        string Name,
        string Number,
        string ExpiryYear,
        string ExpiryMonth,
        string Cvc
    );
}
