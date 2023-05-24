using MediatR;
using ProductsPricing.Application.Commands.Requests;
using ProductsPricing.Application.Commands.Responses;
using ProductsPricing.Core.Integrations.Events;
using ProductsPricing.Domain.Imports.Commands.Requests;

namespace ProductsPricing.Application.Commands.Handlers
{
    public class ImportSpedFileCommandHandler : IRequestHandler<ImportSpedFileCommandRequest, ImportSpedFileCommandResponse>
    {
        private readonly IMediator _mediator;

        public ImportSpedFileCommandHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<ImportSpedFileCommandResponse> Handle(ImportSpedFileCommandRequest request, CancellationToken cancellationToken)
        {
            var fileContent = new List<string>();

            using (var reader = new StreamReader(request.File.OpenReadStream()))
            {

                while (!reader.EndOfStream)
                {
                    string? line = await reader.ReadLineAsync();

                    fileContent.Add(line);

                }
                
            }

            var response = await _mediator.Send(new CreateImportCommandRequest { FileName = request.File.FileName, FileContent = fileContent }, CancellationToken.None);

            return new ImportSpedFileCommandResponse { ImportId = response.ImportId };
        }
    }
}
