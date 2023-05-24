using MediatR;
using ProductsPricing.Domain.Imports.Commands.Responses;

namespace ProductsPricing.Domain.Imports.Commands.Requests
{
    public class CreateImportCommandRequest : IRequest<CreateImportCommandResponse>
    {
        public List<string> FileContent { get; set; }
        public string FileName { get; set; }
    }
}