using ProductsPricing.Core.DomainObjects;
using ProductsPricing.Core.ValueObjects;

namespace ProductsPricing.Domain.Imports.Entities
{
    public class ImportLog : Entity
    {
        protected ImportLog()
        { }

        public ImportLog(Import import, LogLevel logLevel, string message)
        {
            UpdateImport(import);
            UpdateMessage(message);
            UpdateLogLevel(logLevel);
        }

        public Guid ImportId { get; private set; }
        public Import Import { get; private set; }
        public LogLevel LogLevel { get; private set; }
        public string Message { get; private set; }

        private void UpdateImport(Import import)
        {
            if(import is null)
            {
                throw new ArgumentNullException(nameof(import));
            }

            ImportId = import.Id;
            Import = import;
        }

        private void UpdateLogLevel(LogLevel logLevel)
        {
            if (logLevel is null)
            {
                throw new ArgumentNullException(nameof(logLevel));
            }

            LogLevel = logLevel;
        }
        private void UpdateMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message));
            }

            Message = message;
        }
    }
}