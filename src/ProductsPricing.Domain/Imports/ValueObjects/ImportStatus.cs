using ProductsPricing.Core.ValueObjects;

namespace ProductsPricing.Domain.Imports.ValueObjects
{
    public class ImportStatus : Enumeration<int>
    {
        private ImportStatus(string name, int value) : base(name, value)
        {
        }

        public static ImportStatus Pending() => new("Pending", 1);
        public static ImportStatus Running() => new("Running", 2);
        public static ImportStatus Failure() => new("Failure", 3);
        public static ImportStatus Success() => new("Success", 4);
        public bool IsPending() => Value == 1;
        public bool IsRunning() => Value == 2;
        public bool IsFailure() => Value == 3;
        public bool IsSuccess() => Value == 4;
    }
}
