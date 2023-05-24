using ProductsPricing.Core.ValueObjects;

namespace ProductsPricing.Domain.Imports.ValueObjects
{
    public class PendingImportItemStatus : Enumeration<int>
    {
        private PendingImportItemStatus(string name, int value) : base(name, value)
        {
        }

        public static PendingImportItemStatus Pending() => new("Pending", 1);
        public static PendingImportItemStatus Selected() => new("Selected", 2);
        public bool IsPending() => Value == 1;
        public bool IsSelected() => Value == 2;
    }
}
