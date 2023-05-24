using ProductsPricing.Core.ValueObjects;

namespace ProductsPricing.Domain.Imports.ValueObjects
{
    public class ImpactedProductStatus : Enumeration<int>
    {
        private ImpactedProductStatus(string name, int value) : base(name, value)
        {
        }

        public static ImpactedProductStatus Pending() => new("Pending", 1);
        public static ImpactedProductStatus Recalculated() => new("Recalculated", 2);
        public bool IsPending() => Value == 1;
        public bool IsRecalculated() => Value == 2;


    }
}
