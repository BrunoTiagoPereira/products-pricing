using MediatR;
using ProductsPricing.Core.Exceptions;
using ProductsPricing.Domain.Contracts.Repositories;
using ProductsPricing.Domain.Sped.Commands.Requests;
using ProductsPricing.Domain.Sped.Commands.Responses;
using System.Text;

namespace ProductsPricing.Domain.Sped.Commands.Handlers
{
    public class ThrowIfAnySpedItemHasInvalidNcmCommandHandler : IRequestHandler<ThrowIfAnySpedItemHasInvalidNcmCommandRequest, ThrowIfAnySpedItemHasInvalidNcmCommandResponse>
    {
        private readonly INcmRepository _ncmRepository;

        public ThrowIfAnySpedItemHasInvalidNcmCommandHandler(INcmRepository ncmRepository)
        {
            _ncmRepository = ncmRepository ?? throw new ArgumentNullException(nameof(ncmRepository));
        }

        public async Task<ThrowIfAnySpedItemHasInvalidNcmCommandResponse> Handle(ThrowIfAnySpedItemHasInvalidNcmCommandRequest request, CancellationToken cancellationToken)
        {
            var ncms = request.Items.Select(x => x.Ncm);

            var existingNcmCodeValues = _ncmRepository.GetExistingNcmCodeValuesFromItems(ncms);

            var notExistingNcmCodeValues = GetNotExistingNcmCodeValuesFromItems(ncms, existingNcmCodeValues);

            if (notExistingNcmCodeValues.Any())
            {
                var formattedNcmCodeValues = GetFormattedNcmCodeValuesFromItems(notExistingNcmCodeValues);

                throw new DomainException($"Há registros com NCMs inválidos ou não cadastrados. Ncms inválidos: \n{formattedNcmCodeValues}");
            }

            return new ThrowIfAnySpedItemHasInvalidNcmCommandResponse();
        }

        private static string GetFormattedNcmCodeValuesFromItems(IEnumerable<string> items)
        {
            StringBuilder sb = new();

            foreach (var item in items)
            {
                sb.AppendLine(item);
            }

            return sb.ToString();
        }

        private static IEnumerable<string> GetNotExistingNcmCodeValuesFromItems(IEnumerable<string> items, IEnumerable<string> existingNcmCodes)
        {
            return items.Where(x => !existingNcmCodes.Contains(x));
        }
    }
}