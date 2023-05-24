using FluentAssertions;
using ProductsPricing.Core.DomainObjects;
using ProductsPricing.UnitTests.Core.Fakers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProductsPricing.Core.Tests.DomainObjects
{
    public class EntityTests
    {
        [Fact]
        public void Should_create_event()
        {
            // Given / When
            var entity = new EntityFake();

            // Then
            entity.Id.Should().NotBeEmpty();
            entity.CreatedAt.Should().NotBeSameDateAs(DateTime.MinValue);
            entity.Events.Should().BeEmpty();
        }

        [Fact]
        public void Should_throw_when_adding_null_event()
        {
            // Given / When
            var entity = new EntityFake();
            var action = () => entity.AddEvent(null);

            // Then
            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Should_add_event()
        {
            // Given
            var entity = new EntityFake();
            var @event = new EventFake();

            // When
            entity.AddEvent(@event);

            // Then
            entity.Events.Should().NotBeEmpty();
            entity.Events.Should().ContainSingle();
            entity.Events.First().Should().Be(@event);
        }

        [Fact]
        public void Should_clear_events()
        {
            // Given
            var entity = new EntityFake();
            var @event = new EventFake();
            entity.AddEvent(@event);

            // When
            entity.ClearEvents();

            // Then
            entity.Events.Should().BeEmpty();
        }
    }
}
