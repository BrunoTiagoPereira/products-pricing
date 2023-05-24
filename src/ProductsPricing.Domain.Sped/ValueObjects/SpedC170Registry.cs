namespace ProductsPricing.Domain.Sped.ValueObjects
{
    public class SpedC170Registry : SpedRegistry
    {
        public const string REGISTRY_ID = "|C170|";
        public decimal Value { get; private set; }
        public decimal DiscountValue { get; private set; }
        public decimal IcmsValue { get; private set; }
        public decimal ST { get; private set; }
        public decimal IpiValue { get; private set; }
        public decimal PisValue { get; private set; }
        public decimal CofinsValue { get; private set; }
        public int FileLineReference { get; set; }
        public static bool IsLineFromC170Registry(string line)
        {
            return line.StartsWith(REGISTRY_ID);
        }

        public static SpedC170Registry From(string line, int fileLineReference)
        {
            ThrowIfLineIsNotFromRegistry(line, REGISTRY_ID);

            return new SpedC170Registry
            {
                Code = GetStringValueFromPosition(line, 3),
                Value = GetDecimalValueFromPosition(line, 7),
                DiscountValue = GetDecimalValueFromPosition(line, 8),
                IcmsValue = GetDecimalValueFromPosition(line, 15),
                ST = GetDecimalValueFromPosition(line, 18),
                IpiValue = GetDecimalValueFromPosition(line, 24),
                PisValue = GetDecimalValueFromPosition(line, 30),
                CofinsValue = GetDecimalValueFromPosition(line, 36),
                FileLineReference = fileLineReference
            };
        }
    }
}