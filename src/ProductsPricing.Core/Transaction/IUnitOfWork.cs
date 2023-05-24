namespace ProductsPricing.Core.Transaction
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync();
    }
}