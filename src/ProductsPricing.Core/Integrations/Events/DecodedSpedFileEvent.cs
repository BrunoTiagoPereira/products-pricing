using MediatR;
using ProductsPricing.Core.DomainObjects;

namespace ProductsPricing.Core.Integrations.Events
{
    public class DecodedSpedFileEvent : Event
    {
        public List<string> FileContent { get; set; }
    }
}
