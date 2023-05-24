//using MediatR;
//using ProductsPricing.Domain.Contracts.JobScheduler;
//using ProductsPricing.Domain.Contracts.Repositories;
//using ProductsPricing.Domain.Imports.Commands.Requests;
//using ProductsPricing.Domain.Imports.Commands.Responses;
//using ProductsPricing.Domain.Imports.Entities;

//namespace ProductsPricing.Domain.Imports.Commands.Handlers
//{
//    public class CreateRecalculationJobForImportEvaluatedItemsCommandHandler : IRequestHandler<CreateRecalculationJobForImportEvaluatedItemsCommandRequest, CreateRecalculationJobForImportEvaluatedItemsCommandResponse>
//    {
//        private readonly IImportRepository _importRepository;
//        private readonly IJobScheduler _jobScheduler;

//        public CreateRecalculationJobForImportEvaluatedItemsCommandHandler(IImportRepository importRepository, IJobScheduler jobScheduler)
//        {
//            _importRepository = importRepository ?? throw new ArgumentNullException(nameof(importRepository));
//            _jobScheduler = jobScheduler ?? throw new ArgumentNullException(nameof(jobScheduler));
//        }

//        public async Task<CreateRecalculationJobForImportEvaluatedItemsCommandResponse> Handle(CreateRecalculationJobForImportEvaluatedItemsCommandRequest request, CancellationToken cancellationToken)
//        {
//            var import = await _importRepository.FindAsync(request.ImportId);

//            var evaluatedImportItems = GetEvaluatedImportItems(import);

//            foreach (var evaluatedImportItem in evaluatedImportItems)
//            {
//                _jobScheduler.EnqueueCommand((x) => x.Send(new RecalculateProductValueCommandRequest { ProductId = evaluatedImportItem.ProductId }));
//            }

//            return new CreateRecalculationJobForImportEvaluatedItemsCommandResponse();
//        }

//        private static IEnumerable<EvaluatedImportItem> GetEvaluatedImportItems(Import import)
//        {
//            return import.Items.Where(x => x is EvaluatedImportItem).Cast<EvaluatedImportItem>();
//        }
//    }
//}