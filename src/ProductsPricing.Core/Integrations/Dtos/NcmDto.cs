using System.Text.Json.Serialization;

namespace ProductsPricing.Core.Integrations.Dtos
{
    public class NcmDto
    {
        [JsonPropertyName("Nomenclaturas")]
        public List<NcmItemDto> Items { get; set; }
    }

    public class NcmItemDto
    {
        [JsonPropertyName("Codigo")]
        public string Code { get; set; }

        [JsonPropertyName("Descricao")]
        public string Description { get; set; }
    }
}