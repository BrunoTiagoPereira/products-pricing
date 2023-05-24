using MediatR;
using ProductsPricing.Core.Transaction;
using ProductsPricing.Domain.Contracts.Repositories;
using ProductsPricing.Domain.Imports.Commands.Requests;
using ProductsPricing.Domain.Imports.Commands.Responses;

namespace ProductsPricing.Domain.Imports.Commands.Handlers
{
    public class UpdateProductValuesFromEvaluatedItemsCommandHandler : IRequestHandler<UpdateProductValuesFromEvaluatedItemsCommandRequest, UpdateProductValuesFromEvaluatedItemsCommandResponse>
    {
        private readonly IImportRepository _importRepository;
        private readonly IUnitOfWork _uow;

        public UpdateProductValuesFromEvaluatedItemsCommandHandler(IImportRepository importRepository, IUnitOfWork uow)
        {
            _importRepository = importRepository ?? throw new ArgumentNullException(nameof(importRepository));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task<UpdateProductValuesFromEvaluatedItemsCommandResponse> Handle(UpdateProductValuesFromEvaluatedItemsCommandRequest request, CancellationToken cancellationToken)
        {
            var evaluatedItems = _importRepository.GetAllEvaluatedImportItemsWithProductsFromImport(request.ImportId);

            foreach (var evaluatedItem in evaluatedItems)
            {
                evaluatedItem.Product.UpdateValue(evaluatedItem.NewValue);
            }

            await _uow.CommitAsync();

            return new UpdateProductValuesFromEvaluatedItemsCommandResponse { };
        }
    }
}