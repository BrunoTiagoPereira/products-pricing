namespace ProductsPricing.Core.Domain.Contracts
{
    public interface IFileImportEngine<T> where T : class
    {
        IEnumerable<T> Import(List<string> content);
    }
}
