namespace ProductsPricing.Domain.Sped.ValueObjects
{
    public class Sped0200Registry : SpedRegistry
    {
        public const string REGISTRY_ID = "|0200|";

        public string? Description { get; private set; }
        public string? UnitOfMeasureName { get; private set; }
        public string? Ncm { get; private set; }

        public static bool IsLineFrom0200Registry(string line)
        {
            return line.StartsWith(REGISTRY_ID);
        }
        public static Sped0200Registry From(string line)
        {
            ThrowIfLineIsNotFromRegistry(line, REGISTRY_ID);

            return new Sped0200Registry
            {
                Code = GetStringValueFromPosition(line, 2),
                Description = GetStringValueFromPosition(line, 3),
                UnitOfMeasureName = GetStringValueFromPosition(line, 6),
                Ncm = GetStringValueFromPosition(line, 8),
            };
        }
    }
}