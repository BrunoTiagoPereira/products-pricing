using MediatR;

namespace ProductsPricing.Domain.Contracts.JobScheduler
{
    public interface IJobScheduler
    {
        public void EnqueueCommand(Action<IMediator> action);
    }
}