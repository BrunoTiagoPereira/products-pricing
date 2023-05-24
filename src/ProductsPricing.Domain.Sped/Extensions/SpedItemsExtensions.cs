using ProductsPricing.Core.Exceptions;
using ProductsPricing.Domain.Sped.ValueObjects;
using ProductsPricing.Domain.UnitOfMeasures.Entities;

namespace ProductsPricing.Domain.Sped.Extensions
{
    public static class SpedItemsExtensions
    {
        public static IEnumerable<SpedItem> GetItemsWithNoNcm(this IEnumerable<SpedItem> spedItems)
        {
            return spedItems.Where(x => string.IsNullOrWhiteSpace(x.Ncm)).ToList();
        }

        public static IEnumerable<SpedItem> GetItemsWithNcm(this IEnumerable<SpedItem> spedItems)
        {
            return spedItems.Except(GetItemsWithNoNcm(spedItems));
        }

        public static UnitOfMeasure FindUnitOfMeasureByName(this IEnumerable<UnitOfMeasure> unitOfMeasures, string unitOfMeasureName, StringComparison stringComparison = StringComparison.InvariantCultureIgnoreCase)
        {
            return unitOfMeasures.First(x => x.Name.Equals(unitOfMeasureName, stringComparison));
        }

        public static IEnumerable<SpedRegistry> GetRegistriesFromLines(this List<string> lines)
        {
            var registries = new List<SpedRegistry>();

            for (int i = 0; i < lines.Count; i++)
            {
                AddSpedRegistryFromLine(registries, lines[i], i);
            }

            return registries;
        }

        private static void AddSpedRegistryFromLine(List<SpedRegistry> registries, string line, int fileLineReference)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                return;
            }
            else if (SpedC170Registry.IsLineFromC170Registry(line))
            {
                registries.Add(SpedC170Registry.From(line, fileLineReference + 1));
            }
            else if (Sped0200Registry.IsLineFrom0200Registry(line))
            {
                registries.Add(Sped0200Registry.From(line));
            }
        }

        public static IEnumerable<SpedC170Registry> GetC170Registries(this IEnumerable<SpedRegistry> registries)
        {
            return registries
                .Where(x => x is SpedC170Registry)
                .Cast<SpedC170Registry>();
        }

        public static IEnumerable<Sped0200Registry> Get0200Registries(this IEnumerable<SpedRegistry> registries)
        {
            return registries
                .Where(x => x is Sped0200Registry)
                .Cast<Sped0200Registry>();
        }

        public static Sped0200Registry Get0200RegistryCorrespondence(this IEnumerable<SpedRegistry> registries, SpedC170Registry spedC170Registry)
        {
            var registry = registries.FirstOrDefault(x => x is Sped0200Registry && x.Code == spedC170Registry.Code);

            if (registry is null)
            {
                throw new DomainException($"O registro C170 da linha {spedC170Registry.FileLineReference} não tem registro 0200 correspondente.");
            }

            return (Sped0200Registry)registry;
        }


    }
}