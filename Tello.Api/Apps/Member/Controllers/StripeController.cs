
using Microsoft.AspNetCore.Mvc;
using Tello.Service.Client.Member.Implementations;
using Tello.Service.Client.Member.Resources;

namespace Tello.Api.Apps.Member.Controllers
{
    [Route("api/stripe")]
    [ApiController]
    public class StripeController : ControllerBase
    {
        private readonly IStripeService _stripeService;

        public StripeController(IStripeService stripeService)
        {
            _stripeService = stripeService;
        }

        [HttpPost("customer")]
        public async Task<ActionResult<CustomerResource>> CreateCustomer([FromBody] CreateCustomerResource resource,
            CancellationToken cancellationToken)
        {
            var response = await _stripeService.CreateCustomer(resource, cancellationToken);
            return Ok(response);
        }

        [HttpPost("charge")]
        public async Task<ActionResult<ChargeResource>> CreateCharge([FromBody] CreateChargeResource resource, CancellationToken cancellationToken)
        {
            var response = await _stripeService.CreateCharge(resource, cancellationToken);
            return Ok(response);
        }

    }
}
