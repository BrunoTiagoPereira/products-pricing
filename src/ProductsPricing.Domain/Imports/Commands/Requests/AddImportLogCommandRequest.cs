using MediatR;
using ProductsPricing.Core.ValueObjects;
using ProductsPricing.Domain.Imports.Commands.Responses;

namespace ProductsPricing.Domain.Imports.Commands.Requests
{
    public class AddImportLogCommandRequest : IRequest<AddImportLogCommandResponse>
    {
        public Guid ImportId { get; set; }
        public LogLevel LogLevel { get; set; }
        public string Message { get; set; }
    }
}