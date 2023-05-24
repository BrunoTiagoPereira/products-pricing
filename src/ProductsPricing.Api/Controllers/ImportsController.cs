using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductsPricing.Api.ApiResponses;
using ProductsPricing.Application.Commands.Requests;

namespace ProductsPricing.Api.Controllers
{
    [Route("api/imports")]
    [ApiController]
    [Authorize]
    public class ImportsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ImportsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        [Route("sped")]
        public async Task<ApiResponse> Sped(IFormFile file)
        {
            var result = await _mediator.Send(new ImportSpedFileCommandRequest { File = file });
            return ApiResponse.Success(result.ImportId);
        }

    }
}
