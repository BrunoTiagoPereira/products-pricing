namespace ProductsPricing.Core.DomainObjects
{
    public abstract class Entity
    {
        public Guid Id { get; private set; }

        public DateTime CreatedAt { get; private set; }

        private readonly List<Event> _events;
        public IReadOnlyCollection<Event> Events => _events.AsReadOnly();

        public Entity()
        {
            Id = Guid.NewGuid();

            CreatedAt = DateTime.Now;

            _events = new List<Event>();
        }

        public void AddEvent(Event @event)
        {
            if (@event is null)
            {
                throw new InvalidOperationException("O evento não pode ser nulo");
            }

            _events.Add(@event);
        }

        public void ClearEvents()
        {
            _events.Clear();
        }
    }
}