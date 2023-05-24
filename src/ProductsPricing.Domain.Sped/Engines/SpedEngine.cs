using ProductsPricing.Core.Domain.Contracts;
using ProductsPricing.Domain.Sped.Extensions;
using ProductsPricing.Domain.Sped.ValueObjects;

namespace ProductsPricing.Domain.Sped.Engines
{
    public class SpedEngine : IFileImportEngine<SpedItem>
    {
        public IEnumerable<SpedItem> Import(List<string> content)
        {
            var registries = content.GetRegistriesFromLines();

            var spedItems = GetSpedItemsFromRegistries(registries);

            return spedItems;
        }


        private static IEnumerable<SpedItem> GetSpedItemsFromRegistries(
            IEnumerable<SpedRegistry> registries)
        {
            var spedItems = new List<SpedItem>();

            var spedC170Registries = registries.GetC170Registries();

            foreach (var spedC170Registry in spedC170Registries)
            {
                var correspondence = registries.Get0200RegistryCorrespondence(spedC170Registry);

                var spedItem = new SpedItem
                {
                    Ncm = correspondence.Ncm,
                    Description = correspondence.Description,
                    UnitOfMeasureName = correspondence.UnitOfMeasureName,
                    FileLineReference = spedC170Registry.FileLineReference,
                    DiscountValue = spedC170Registry.DiscountValue,
                    Value = spedC170Registry.Value,
                    IcmsValue = spedC170Registry.IcmsValue,
                    IpiValue = spedC170Registry.IpiValue,
                    PisValue = spedC170Registry.PisValue,
                    CofinsValue = spedC170Registry.CofinsValue,
                    ST = spedC170Registry.ST
                };

                spedItems.Add(spedItem);
            }

            return spedItems;
        }
    }
}