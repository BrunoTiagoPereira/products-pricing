using ProductsPricing.Core.Domain.Contracts;
using ProductsPricing.Core.Exceptions;
using ProductsPricing.Domain.Sintegra.Dtos;
using System.Text;

namespace ProductsPricing.Domain.Sintegra.Engines
{
    public class SintegraEngine : IFileImportEngine<SintegraItem>
    {
        public IEnumerable<SintegraItem> Import(List<string> registries)
        {
            var sintegra54Registries = GetSintegra54RegistryItems(registries);
            var sintegra75Registries = GetSintegra75RegistryItems(registries);

            var sintegraItems = GetSintegraItemsFromRegistries(sintegra54Registries, sintegra75Registries);

            return sintegraItems;
        }

        private static List<SintegraItem> GetSintegraItemsFromRegistries(IEnumerable<Sintegra54Registry> sintegra54Registries, IEnumerable<Sintegra75Registry> sintegra75Registries)
        {
            ValidateProductCodeCorrespondence(sintegra54Registries, sintegra75Registries);
            
            var sintegraItems = new List<SintegraItem>();

            foreach (var sintegra54Registry in sintegra54Registries)
            {
                var sintegra75Registry = sintegra75Registries.First(x => x.ProductCode == sintegra54Registry.ProductCode);

                sintegraItems.Add(new SintegraItem
                {
                    Ncm = sintegra75Registry.Ncm,
                    UnitOfMeasure = sintegra75Registry.UnitOfMeasure,
                    Value = sintegra54Registry.Value,
                    AdditionalValue = sintegra54Registry.AdditionalValue,
                    IpiValue = sintegra54Registry.IpiValue
                });
            }
            return sintegraItems;
        }

        private static void ValidateProductCodeCorrespondence(IEnumerable<Sintegra54Registry> sintegra54Registries, IEnumerable<Sintegra75Registry> sintegra75Registries)
        {
            var sintegra54ProductCodes = sintegra54Registries
                .Select(x => x.ProductCode)
                .Distinct();

            var sintegra75ProductCodes = sintegra54Registries
               .Select(x => x.ProductCode)
               .Distinct();

            var invalidProductCodes = sintegra54ProductCodes
                .Where(x => !sintegra75ProductCodes.Contains(x));

            if (invalidProductCodes.Any())
            {
                var sb = new StringBuilder();
                foreach (var productCode in invalidProductCodes)
                {
                    sb.AppendLine(productCode);
                }

                throw new DomainException($"Há itens no registro {Sintegra54Registry.REGISTRY_CODE} sem correspondência no registro ${Sintegra75Registry.REGISTRY_CODE}. Códigos dos produtos no sintegra: {sb}");
            }
        }
        private static IEnumerable<string> GetLinesByRegistryCode(List<string> content, string registryCode)
        {
            return content.Where(x => !string.IsNullOrWhiteSpace(x) && x.StartsWith(registryCode));
        }

        private static IEnumerable<Sintegra54Registry> GetSintegra54RegistryItems(List<string> registries)
        {
            var sintegra54Registries = GetLinesByRegistryCode(registries, Sintegra54Registry.REGISTRY_CODE);
            return sintegra54Registries.Select(x => Sintegra54Registry.From(x));
        }

        private static IEnumerable<Sintegra75Registry> GetSintegra75RegistryItems(List<string> registries)
        {
            var sintegra75Registries = GetLinesByRegistryCode(registries, Sintegra75Registry.REGISTRY_CODE);
            return sintegra75Registries.Select(x => Sintegra75Registry.From(x));
        }
    }
}
