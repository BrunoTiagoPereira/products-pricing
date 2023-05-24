using MediatR;
using Microsoft.AspNetCore.Http;
using ProductsPricing.Application.Commands.Responses;

namespace ProductsPricing.Application.Commands.Requests
{
    public class ImportSpedFileCommandRequest : IRequest<ImportSpedFileCommandResponse>
    {
        public IFormFile File { get; set; }
    }
}