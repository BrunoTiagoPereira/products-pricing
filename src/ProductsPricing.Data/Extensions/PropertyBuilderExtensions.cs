using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace ProductsPricing.Data.Extensions
{
    public static class PropertyBuilderExtensions
    {
        public static PropertyBuilder<IReadOnlyCollection<T>> HasJsonConversion<T>(this PropertyBuilder<IReadOnlyCollection<T>> propertyBuilder, JsonSerializerOptions? serializerSettings = null)
        {
            return propertyBuilder.HasJsonConversion<IReadOnlyCollection<T>, List<T>>(serializerSettings);
        }

        public static PropertyBuilder<T> HasJsonConversion<T, TComplex>(this PropertyBuilder<T> propertyBuilder, JsonSerializerOptions? serializerSettings = null)
            where TComplex : T
        {
            if (serializerSettings is null)
            {
                serializerSettings = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
            }

            var converter = new ValueConverter<T, string>(
                v => JsonSerializer.Serialize(v, serializerSettings),
                v => JsonSerializer.Deserialize<TComplex>(v, serializerSettings));

            var comparer = new ValueComparer<T>(
                (l, r) => JsonSerializer.Serialize(l, serializerSettings) == JsonSerializer.Serialize(r, serializerSettings),
                v => v == null ? 0 : JsonSerializer.Serialize(v, serializerSettings).GetHashCode(),
                v => JsonSerializer.Deserialize<TComplex>(JsonSerializer.Serialize(v, serializerSettings), serializerSettings)
            );

            propertyBuilder.HasConversion(converter);

            propertyBuilder.Metadata.SetValueConverter(converter);
            propertyBuilder.Metadata.SetValueComparer(comparer);

            return propertyBuilder;
        }
    }
}