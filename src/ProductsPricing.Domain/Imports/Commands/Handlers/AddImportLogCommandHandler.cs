using MediatR;
using ProductsPricing.Core.Transaction;
using ProductsPricing.Core.ValueObjects;
using ProductsPricing.Domain.Contracts.Repositories;
using ProductsPricing.Domain.Imports.Commands.Requests;
using ProductsPricing.Domain.Imports.Commands.Responses;
using ProductsPricing.Domain.Imports.Entities;
using ProductsPricing.Domain.Users.Managers;

namespace ProductsPricing.Domain.Imports.Commands.Handlers
{
    public class AddImportLogCommandHandler : IRequestHandler<AddImportLogCommandRequest, AddImportLogCommandResponse>
    {
        private readonly IImportRepository _importRepository;
        private readonly IUnitOfWork _uow;

        public AddImportLogCommandHandler(IImportRepository importRepository, IUnitOfWork uow)
        {
            _importRepository = importRepository ?? throw new ArgumentNullException(nameof(importRepository));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task<AddImportLogCommandResponse> Handle(AddImportLogCommandRequest request, CancellationToken cancellationToken)
        {
            var import = await _importRepository.FindAsync(request.ImportId);

            import.AddLog(request.LogLevel, request.Message);
            _importRepository.Update(import);

            await _uow.CommitAsync();

            return new AddImportLogCommandResponse { };
        }
    }
}