using ProductsPricing.Core.DomainObjects;
using ProductsPricing.Core.Exceptions;
using ProductsPricing.Core.ValueObjects;
using ProductsPricing.Domain.Imports.ValueObjects;
using ProductsPricing.Domain.Users.Contracts;
using ProductsPricing.Domain.Users.Entities;

namespace ProductsPricing.Domain.Imports.Entities
{
    public class Import : AggregateRoot, IUserRelated
    {
        public const int FILE_NAME_MAX_LENGTH = 512;
        protected Import()
        {
            _items = new List<ImportItem>();
            _logs = new List<ImportLog>();
            _impactedProducts = new List<ImpactedProduct>();
        }
        public Import(string fileName, User user)
        {
            UpdateFileName(fileName);
            UpdateUser(user);

            _items = new List<ImportItem>();
            _logs = new List<ImportLog>();
            _impactedProducts = new List<ImpactedProduct>();


            Start();
        }

        public DateTime StartedAt { get; private set; }
        public DateTime FinishedAt { get; private set; }
        public string FileName { get; private set; }
        public Guid UserId { get; private set; }
        public User User { get; private set; }

        public ImportStatus Status { get; private set; }

        private readonly List<ImportItem> _items;
        public IReadOnlyCollection<ImportItem> Items => _items.AsReadOnly();

        private readonly List<ImportLog> _logs;
        public IReadOnlyCollection<ImportLog> Logs => _logs.AsReadOnly();

        private readonly List<ImpactedProduct> _impactedProducts;
        public IReadOnlyCollection<ImpactedProduct> ImpactedProducts => _impactedProducts.AsReadOnly();

        public void AddItem(ImportItem importItem)
        {
            if(importItem is null)
            {
                throw new ArgumentNullException(nameof(importItem));
            }

            _items.Add(importItem);
        }
        public void AddLog(LogLevel logLevel, string message)
        {
            if (logLevel is null)
            {
                throw new ArgumentNullException(nameof(logLevel));
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message));
            }

            _logs.Add(new ImportLog(this, logLevel, message));
        }
        public void AddInformationLog(string message) => AddLog(LogLevel.Information(), message);
        public void AddWarningLog(string message) => AddLog(LogLevel.Warning(), message);
        public void AddErrorLog(string message) => AddLog(LogLevel.Error(), message);
        public void MarkAsFailure(string errorMessage)
        {
            if (Status.IsSuccess())
            {
                throw new DomainException("Não é possível marcar uma importação como erro depois de finalizada.");
            }

            Status = ImportStatus.Failure();
            AddErrorLog(errorMessage);
            InternalFinish();
        }

        public void MarkAsCancelled()
        {
            if (Status.IsSuccess())
            {
                throw new DomainException("Não é possível marcar uma importação como cancelada depois de finalizada.");
            }

            Status = ImportStatus.Failure();
            AddErrorLog("Importação cancelada.");
            InternalFinish();
        }
        public void Finish()
        {
            if (_items.Any(x => x is PendingImportItem))
            {
                throw new DomainException("Importação ainda não acabou ou há itens pendentes.");
            }

            Status = ImportStatus.Success();
            InternalFinish();

        }
        private void UpdateUser(User user)
        {
            if(user is null)
            {
                throw new ArgumentNullException("O usuário deve ser válido.");
            }

            UserId = user.Id;
            User = user;
        }
        private void UpdateFileName(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException("O nome do arquivo deve ser válido.");
            }

            if(fileName.Length > FILE_NAME_MAX_LENGTH)
            {
                throw new DomainException($"O nome do arquivo não pode ter mais que {FILE_NAME_MAX_LENGTH} caracteres.");
            }

            FileName = fileName;
        }
        private void Start()
        {
            StartedAt = DateTime.Now;
            Status = ImportStatus.Running();
            AddInformationLog($"Importação inciada em {StartedAt:dd/mm/yyyy hh:mm:ss}");
        }
        private void InternalFinish()
        {
            FinishedAt = DateTime.Now;
            AddInformationLog($"Importação finalizada em {FinishedAt:dd/mm/yyyy hh:mm:ss}");
        }

        
       


    }
}