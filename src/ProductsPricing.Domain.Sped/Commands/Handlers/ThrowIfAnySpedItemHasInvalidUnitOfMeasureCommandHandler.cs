using MediatR;
using ProductsPricing.Core.Exceptions;
using ProductsPricing.Domain.Contracts.Repositories;
using ProductsPricing.Domain.Sped.Commands.Requests;
using ProductsPricing.Domain.Sped.Commands.Responses;
using ProductsPricing.Domain.Sped.ValueObjects;
using ProductsPricing.Domain.UnitOfMeasures.Entities;
using System.Text;

namespace ProductsPricing.Domain.Sped.Commands.Handlers
{
    public class ThrowIfAnySpedItemHasInvalidUnitOfMeasureCommandHandler : IRequestHandler<ThrowIfAnySpedItemHasInvalidUnitOfMeasureCommandRequest, ThrowIfAnySpedItemHasInvalidUnitOfMeasureCommandResponse>
    {
        private readonly IUnitOfMeasureRepository _unitOfMeasureRepository;

        public ThrowIfAnySpedItemHasInvalidUnitOfMeasureCommandHandler(IUnitOfMeasureRepository unitOfMeasureRepository)
        {
            _unitOfMeasureRepository = unitOfMeasureRepository ?? throw new ArgumentNullException(nameof(unitOfMeasureRepository));
        }

        public async Task<ThrowIfAnySpedItemHasInvalidUnitOfMeasureCommandResponse> Handle(ThrowIfAnySpedItemHasInvalidUnitOfMeasureCommandRequest request, CancellationToken cancellationToken)
        {
            var userUnitOfMeasures = _unitOfMeasureRepository.GetAllFromUser(request.UserId);

            var invalidUnitOfMeasures = GetInvalidUnitOfMeasuresFromItems(userUnitOfMeasures, request.Items);

            if (invalidUnitOfMeasures.Any())
            {
                string invalidUnitOfMeasureNames = GetInvalidUnitOfMeasuresNames(invalidUnitOfMeasures);

                throw new DomainException($"Há registros com unidades de medida inválidas ou não cadastradas. Unidades de medida inválidas: \n{invalidUnitOfMeasureNames}");
            }

            return new ThrowIfAnySpedItemHasInvalidUnitOfMeasureCommandResponse();
        }

        private static string GetInvalidUnitOfMeasuresNames(IEnumerable<string> invalidUnitOfMeasures)
        {
            StringBuilder sb = new();

            foreach (var invalidUnitOfMeasure in invalidUnitOfMeasures)
            {
                sb.AppendLine(invalidUnitOfMeasure);
            }

            return sb.ToString();
        }

        private static IEnumerable<string> GetInvalidUnitOfMeasuresFromItems(IEnumerable<UnitOfMeasure> userUnitOfMeasures, IEnumerable<SpedItem> items)
        {
            var validUnitOfMeasuresNames = userUnitOfMeasures.Select(x => x.Name).ToList();

            return items
                .Where(x => !validUnitOfMeasuresNames.Contains(x.UnitOfMeasureName))
                .Select(x => x.UnitOfMeasureName)
                ;
        }
    }
}