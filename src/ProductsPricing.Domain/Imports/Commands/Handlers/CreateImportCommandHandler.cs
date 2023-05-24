using MediatR;
using ProductsPricing.Core.Integrations.Events;
using ProductsPricing.Core.Transaction;
using ProductsPricing.Domain.Contracts.Repositories;
using ProductsPricing.Domain.Imports.Commands.Requests;
using ProductsPricing.Domain.Imports.Commands.Responses;
using ProductsPricing.Domain.Imports.Entities;
using ProductsPricing.Domain.Users.Managers;

namespace ProductsPricing.Domain.Imports.Commands.Handlers
{
    public class CreateImportCommandHandler : IRequestHandler<CreateImportCommandRequest, CreateImportCommandResponse>
    {
        private readonly IUserAccessorManager _userAccessorManager;
        private readonly IImportRepository _importRepository;
        private readonly IUnitOfWork _uow;

        public CreateImportCommandHandler(IUserAccessorManager userAccessorManager, IImportRepository importRepository, IUnitOfWork uow)
        {
            _userAccessorManager = userAccessorManager ?? throw new ArgumentNullException(nameof(userAccessorManager));
            _importRepository = importRepository ?? throw new ArgumentNullException(nameof(importRepository));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task<CreateImportCommandResponse> Handle(CreateImportCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _userAccessorManager.GetCurrentUser();

            var import = new Import(request.FileName, user);

            import.AddEvent(new DecodedSpedFileEvent { AggregateRootId = import.Id, FileContent = request.FileContent });

            await _importRepository.AddAsync(import);
            await _uow.CommitAsync();

            return new CreateImportCommandResponse { ImportId = import.Id };
        }
    }
}