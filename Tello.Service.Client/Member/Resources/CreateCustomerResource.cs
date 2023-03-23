
namespace Tello.Service.Client.Member.Resources
{
    public record CreateCustomerResource
    (
        string Email,
        string Name,
        CreateCardResource Card
    );
}
