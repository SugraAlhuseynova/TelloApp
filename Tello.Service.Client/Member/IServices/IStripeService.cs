using Tello.Service.Client.Member.Resources;

namespace Tello.Service.Client.Member.Implementations
{
    public interface IStripeService
    {

        Task<CustomerResource> CreateCustomer(CreateCustomerResource resource, CancellationToken cancellationToken);
        Task<ChargeResource> CreateCharge(CreateChargeResource resource, CancellationToken cancellationToken);

    }
}
