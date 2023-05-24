using MediatR;
using ProductsPricing.Core.Transaction;
using ProductsPricing.Domain.Contracts.Repositories;
using ProductsPricing.Domain.Imports.Commands.Requests;
using ProductsPricing.Domain.Imports.Commands.Responses;
using ProductsPricing.Domain.Imports.Entities;
using ProductsPricing.Domain.Users.Managers;

namespace ProductsPricing.Domain.Imports.Commands.Handlers
{
    public class RecalculateProductValueCommandHandler : IRequestHandler<RecalculateProductValueCommandRequest, RecalculateProductValueCommandResponse>
    {
        private readonly IUserAccessorManager _userAccessorManager;
        private readonly IImportRepository _importRepository;
        private readonly IUnitOfWork _uow;

        public RecalculateProductValueCommandHandler(IUserAccessorManager userAccessorManager, IImportRepository importRepository, IUnitOfWork uow)
        {
            _userAccessorManager = userAccessorManager ?? throw new ArgumentNullException(nameof(userAccessorManager));
            _importRepository = importRepository ?? throw new ArgumentNullException(nameof(importRepository));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task<RecalculateProductValueCommandResponse> Handle(RecalculateProductValueCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();

            return new RecalculateProductValueCommandResponse();
        }
    }
}