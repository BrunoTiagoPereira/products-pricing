//using MediatR;
//using ProductsPricing.Core.Domain.Contracts;
//using ProductsPricing.Core.Exceptions;
//using ProductsPricing.Core.Integrations.Events;
//using ProductsPricing.Domain.Contracts.Repositories;
//using ProductsPricing.Domain.Ncms.Entities;
//using ProductsPricing.Domain.Products.ValueObjects;
//using ProductsPricing.Domain.Sintegra.Commands.Requests;
//using ProductsPricing.Domain.Sintegra.Dtos;

//namespace ProductsPricing.Domain.Sintegra.Events.Handlers
//{
//    public class ParsedSintegraEventHandler : INotificationHandler<DecodedSpedFileEvent>
//    {
//        private readonly INcmRepository _ncmRepository;
//        private readonly IFileImportEngine<SintegraItem> _engine;
//        private readonly IMediator _mediator;

//        public ParsedSintegraEventHandler(INcmRepository ncmRepository, IFileImportEngine<SintegraItem> engine, IMediator mediator)
//        {
//            _ncmRepository = ncmRepository ?? throw new ArgumentNullException(nameof(ncmRepository));
//            _engine = engine ?? throw new ArgumentNullException(nameof(engine));
//            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
//        }

//        public async Task Handle(DecodedSpedFileEvent notification, CancellationToken cancellationToken)
//        {
//            var sintegraItems = _engine.Import(notification.FileContent);

//            ThrowIfHasAnyInvalidUnitOfMeasure(sintegraItems);
//            await ThrowIfHasAnyInvalidNcm(sintegraItems);

//            _mediator.Send(new CreateSintegraProductsFromItemsCommandRequest(), CancellationToken.None);
//        }
//        private void ThrowIfHasAnyInvalidUnitOfMeasure(IReadOnlyCollection<SintegraItem> sintegraItems)
//        {
//            var unitOfMeasures = UnitOfMeasure.GetAllUnitOfMeasures();
//            var invalidUnitOfMeasures = sintegraItems
//                .Where(x => !unitOfMeasures.Contains(x.UnitOfMeasure))
//                .Select(x => x.UnitOfMeasure)
//                ;

//            if (invalidUnitOfMeasures.Any())
//            {
//                throw new DomainException($"Há registros com unidades de medida inválidas ou não cadastradas. Unidades de medida inválidas: \n{invalidUnitOfMeasures.Aggregate((a, b) => $"{a}\n{b}")}");
//            }

//        }
//        private async Task ThrowIfHasAnyInvalidNcm(IReadOnlyCollection<SintegraItem> sintegraItems)
//        {
//            var ncms = sintegraItems.Select(x => x.Ncm);
//            var invalidNcms = await _ncmRepository.GetInvalidNcmCodesAsync(ncms);

//            if (invalidNcms.Any())
//            {
//                throw new DomainException($"Há registros com NCMs inválidos ou não cadastrados. Ncms inválidos: \n{invalidNcms.Aggregate((a, b) => $"{a}\n{b}")}");
//            }

//        }
//    }
//}
