using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Cartography.Testing
{
    [TestFixture]
    public class EndToEndMappingTester
    {
        [Test]
        public void should_map_entities_with_components_to_data_transfer_objects_with_default_conventions()
        {
            var container = new Container(x => x.AddRegistry<EndToEndMappingRegistry>());
            var provider = container.GetInstance<IMappingProvider>();

            var texas = new State {Id = 1, Name = "Texas"};
            var billing = new Address(texas) {Id = 1, LineOne = "1234 Test Lane", LineTwo = "Apt 123", City = "Austin"};
            var shipping = new Address(texas) { Id = 1, LineOne = "345 Test Drive", LineTwo = "Suite 9000", City = "Austin" };
            var order = new Order
                            {
                                Id = 1000,
                                BillingAddress = billing,
                                ShippingAddress = shipping
                            };

            provider
                .Map<Order, OrderDto>(order)
                .IsEqualTo(order);
        }

        public class EndToEndMappingRegistry : Registry
        {
            public EndToEndMappingRegistry()
            {
                // use default conventions
                this.ConfigureMapping<MappingRegistry>();
            }
        }
    }

    public class Address
    {
        public Address(State state)
        {
            State = state;
        }

        public int Id { get; set; }
        public string LineOne { get; set; }
        public string LineTwo { get; set; }
        public string City { get; set; }
        public State State { get; private set; }
    }

    public class State
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Order
    {
        private readonly IList<OrderLineItem> _lineItems = new List<OrderLineItem>();

        public int Id { get; set; }
        public Address BillingAddress { get; set; }
        public Address ShippingAddress { get; set; }
        public IEnumerable<OrderLineItem> LineItems { get { return _lineItems; } }

        public void AddLineItem(int id, int quantity)
        {
            _lineItems.Fill(new OrderLineItem(this) { Id = id, Quantity = quantity});
        }
    }

    public class OrderLineItem
    {
        public OrderLineItem(Order order)
        {
            Order = order;
        }

        public int Id { get; set; }
        public int Quantity { get; set; }
        public Order Order { get; private set; }
    }

    public class OrderLineItemDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        public bool IsEqualTo(OrderLineItem lineItem)
        {
            return Id.Equals(lineItem.Id) && Quantity.Equals(lineItem.Quantity);
        }
    }

    public class OrderDto
    {
        public int Id { get; set; }
        public AddressDto BillingAddress { get; set; }
        public AddressDto ShippingAddress { get; set; }
        public IEnumerable<OrderLineItemDto> LineItems { get; set; }

        public void IsEqualTo(Order order)
        {
            Id.ShouldEqual(order.Id);
            BillingAddress.IsEqualTo(order.BillingAddress);
            ShippingAddress.IsEqualTo(order.ShippingAddress);

            order
                .LineItems
                .Each(item =>
                          {
                              if (!LineItems.Any(i => i.IsEqualTo(item)))
                              {
                                  Assert.Fail("Invalid order line item.");
                              }

                          });
        }
    }

    public class AddressDto
    {
        public int Id { get; set; }
        public string LineOne { get; set; }
        public string LineTwo { get; set; }
        public string City { get; set; }
        public StateDto State { get; private set; }

        public void IsEqualTo(Address address)
        {
            Id.ShouldEqual(address.Id);
            LineOne.ShouldEqual(address.LineOne);
            LineTwo.ShouldEqual(address.LineTwo);
            City.ShouldEqual(address.City);
            State.IsEqualTo(address.State);
        }
    }

    public class StateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void IsEqualTo(State state)
        {
            Id.ShouldEqual(state.Id);
            Name.ShouldEqual(state.Name);
        }
    }
}