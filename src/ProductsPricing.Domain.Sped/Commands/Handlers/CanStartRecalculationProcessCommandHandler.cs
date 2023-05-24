using MediatR;
using ProductsPricing.Domain.Contracts.Repositories;
using ProductsPricing.Domain.Imports.Entities;
using ProductsPricing.Domain.Imports.ValueObjects;
using ProductsPricing.Domain.Sped.Commands.Requests;
using ProductsPricing.Domain.Sped.Commands.Responses;

namespace ProductsPricing.Domain.Sped.Commands.Handlers
{
    public class CanStartRecalculationProcessCommandHandler : IRequestHandler<CanStartRecalculationProcessCommandRequest, CanStartRecalculationProcessCommandResponse>
    {
        private readonly IImportRepository _importRepository;

        public CanStartRecalculationProcessCommandHandler(IImportRepository importRepository)
        {
            _importRepository = importRepository ?? throw new ArgumentNullException(nameof(importRepository));
        }

        public async Task<CanStartRecalculationProcessCommandResponse> Handle(CanStartRecalculationProcessCommandRequest request, CancellationToken cancellationToken)
        {
            var import = await _importRepository.FindAsync(request.ImportId);

            bool hasNoPendingItems = !import.Items.Any(x => x is PendingImportItem item && item.Status == PendingImportItemStatus.Pending());

            return new CanStartRecalculationProcessCommandResponse { CanStart = hasNoPendingItems };
        }
    }
}