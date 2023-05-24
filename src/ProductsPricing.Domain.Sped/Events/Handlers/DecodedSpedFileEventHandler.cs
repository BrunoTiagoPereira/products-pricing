using MediatR;
using ProductsPricing.Core.Domain.Contracts;
using ProductsPricing.Core.Integrations.Events;
using ProductsPricing.Core.Transaction;
using ProductsPricing.Core.ValueObjects;
using ProductsPricing.Domain.Contracts.Repositories;
using ProductsPricing.Domain.Imports.Commands.Requests;
using ProductsPricing.Domain.Imports.Entities;
using ProductsPricing.Domain.Sped.Commands.Requests;
using ProductsPricing.Domain.Sped.Extensions;
using ProductsPricing.Domain.Sped.ValueObjects;

namespace ProductsPricing.Domain.Sped.Events.Handlers
{
    public class DecodedSpedFileEventHandler : INotificationHandler<DecodedSpedFileEvent>
    {
        private readonly IFileImportEngine<SpedItem> _engine;
        private readonly IMediator _mediator;
        private readonly IImportRepository _importRepository;
        private readonly IUnitOfWork _uow;

        public DecodedSpedFileEventHandler(IFileImportEngine<SpedItem> engine, IMediator mediator, IImportRepository importRepository, IUnitOfWork uow)
        {
            _engine = engine ?? throw new ArgumentNullException(nameof(engine));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _importRepository = importRepository ?? throw new ArgumentNullException(nameof(importRepository));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task Handle(DecodedSpedFileEvent notification, CancellationToken cancellationToken)
        {
            var import = await _importRepository.FindAsync(notification.AggregateRootId);

            try
            {
                await Execute(notification.FileContent, import);
            }
            catch (Exception e)
            {
                await MarkImportAsFailure(import, e.Message);
            }
        }

        private async Task Execute(List<string> fileContent, Import import)
        {
            var spedItems = _engine.Import(fileContent);

            var spedItemsWithNcm = spedItems.GetItemsWithNcm();

            await ThrowIfAnyItemHasInvalidUnitOfMeasure(import.UserId, spedItemsWithNcm);

            await ThrowIfAnyItemHasInvalidNcm(spedItemsWithNcm);

            await AddWarningImportLogForItemsWithNoNcm(import.Id, spedItems);

            await CreateImportItemsFromSpedItems(import.Id, spedItemsWithNcm);

            if (await CanStartRecalculationProcess(import.Id))
            {
                await ThrowIfAnyInvalidUnitOfMeasureConversion(import.Id);

                await CreateEvaluatedItemsFromImportProcessedItems(import.Id);

                await UpdateProductValuesFromEvaluatedItems(import.Id);

                await AddStartingRecalculationProcessImportLog(import.Id);

                //await CreateRecalculationJobForAllEvaluatedProducts(importId);
            }
        }

        private async Task AddWarningImportLogForItemsWithNoNcm(Guid importId, IEnumerable<SpedItem> spedItems)
        {
            await _mediator.Send(new AddWarningImportLogForSpedItemsWithNoNcmCommandRequest { ImportId = importId, SpedItems = spedItems });
        }

        private async Task ThrowIfAnyItemHasInvalidUnitOfMeasure(Guid userId, IEnumerable<SpedItem> spedItems)
        {
            await _mediator.Send(new ThrowIfAnySpedItemHasInvalidUnitOfMeasureCommandRequest { UserId = userId, Items = spedItems });
        }

        private async Task ThrowIfAnyItemHasInvalidNcm(IEnumerable<SpedItem> spedItems)
        {
            await _mediator.Send(new ThrowIfAnySpedItemHasInvalidNcmCommandRequest { Items = spedItems });
        }

        private async Task CreateImportItemsFromSpedItems(Guid importId, IEnumerable<SpedItem> spedItems)
        {
            await _mediator.Send(new CreateImportItemsFromSpedItemsCommandRequest { ImportId = importId, Items = spedItems });
        }

        private async Task<bool> CanStartRecalculationProcess(Guid importId)
        {
            var response = await _mediator.Send(new CanStartRecalculationProcessCommandRequest { ImportId = importId });
            return response.CanStart;
        }

        private async Task ThrowIfAnyInvalidUnitOfMeasureConversion(Guid importId)
        {
            await _mediator.Send(new ThrowIfAnyProcessedItemHasInvalidUnitOfMeasureConversionCommandRequest { ImportId = importId });
        }

        private async Task CreateEvaluatedItemsFromImportProcessedItems(Guid importId)
        {
            await _mediator.Send(new CreateEvaluatedItemsFromProcessedItemsCommandRequest { ImportId = importId });
        }

        private async Task UpdateProductValuesFromEvaluatedItems(Guid importId)
        {
            await _mediator.Send(new UpdateProductValuesFromEvaluatedItemsCommandRequest { ImportId = importId });
        }

        private async Task AddStartingRecalculationProcessImportLog(Guid importId)
        {
            await _mediator.Send(new AddImportLogCommandRequest {
                ImportId = importId,
                LogLevel = LogLevel.Information(),
                Message = "O processo de recálculo foi iniciado." 
            });
        }

        private async Task CreateRecalculationJobForAllEvaluatedProducts(Guid importId)
        {
            await _mediator.Send(new CreateRecalculationJobForImportEvaluatedItemsCommandRequest { ImportId = importId });
        }

        private async Task MarkImportAsFailure(Import import, string errorMessage)
        {
            import.MarkAsFailure(errorMessage);
            _importRepository.Update(import);
            await _uow.CommitAsync();
        }
    }
}