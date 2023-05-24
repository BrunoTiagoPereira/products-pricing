using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductsPricing.Api.ApiResponses;
using ProductsPricing.Domain.Users.Commands.Requests;
using ProductsPricing.Domain.Users.Queries.Requests;

namespace ProductsPricing.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        [Route("")]
        public async Task<ApiResponse> CreateUserAsync([FromBody] CreateUserCommandRequest request)
        {
            await _mediator.Send(request);

            return ApiResponse.Success();
        }

        [HttpPost]
        [Route("login")]
        public async Task<ApiResponse> LoginAsync([FromBody] LoginQueryRequest request)
        {
            var response = await _mediator.Send(request);

            return ApiResponse.Success(response.Token);
        }
    }
}