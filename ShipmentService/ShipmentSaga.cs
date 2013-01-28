using System;
using NServiceBus.Saga;
using PaymentService.Events;
using ShipmentService.Commands;
using ShoppingCartService.Events;

namespace ShipmentService
{
    public class ShipmentSaga : Saga<ShipmentSagaData>,
                             IAmStartedByMessages<IOrderCreatedEvent>, 
                             IAmStartedByMessages<ShipToCommand>,
                             IAmStartedByMessages<IOrderPaidEvent>
                             
    {
        public override void ConfigureHowToFindSaga()
        {
            ConfigureMapping<IOrderCreatedEvent>(s => s.OrderId, m => m.OrderId);
            ConfigureMapping<ShipToCommand>(s => s.OrderId, m => m.OrderId);
            ConfigureMapping<IOrderPaidEvent>(s => s.OrderId, m => m.OrderId);
        }

        public void Handle(IOrderCreatedEvent message)
        {
            RequestUtcTimeout<OrderNotPaidTimeout>(TimeSpan.FromMinutes(30));
            Data.OrderId = message.OrderId;
            Data.OrderTotalAmount = message.TotalAmount;
            Data.OrderReceived = true;
            CheckReadyToShip();
        }

        public void Handle(ShipToCommand message)
        {
            Data.OrderId = message.OrderId;
            Data.ShipTo(message.Name, message.Address, message.Zip, message.City);
            Console.WriteLine("Received address for order" + message.OrderId);
            CheckReadyToShip();

        }

        public void Handle(IOrderPaidEvent message)
        {
            Data.OrderId = message.OrderId;
            Data.AmountPaid = message.AmountPaid;
            Data.Paid = true;
            CheckReadyToShip();
        }

        private void CheckReadyToShip()
        {
            bool readyToShip = Data.OrderReceived && Data.ShipmentAddressReceived && Data.Paid;
            bool hasBeenPaid = Data.OrderTotalAmount == Data.AmountPaid;
            if (readyToShip && hasBeenPaid)
            {
                Bus.SendLocal<ShipViaDHLCommand>(m =>
                                                {
                                                    m.Name = Data.Name;
                                                    m.Address = Data.Address;
                                                    m.City = Data.Address;
                                                    m.OrderId = Data.OrderId;
                                                });
                Console.WriteLine("Shipped order " + Data.OrderId);
                MarkAsComplete();
            }
        }

        public void Timeout(OrderNotPaidTimeout state)
        {
            if (!Data.Paid)
            {
                //oh no, customer never paid. Get customer service to call him and ask why.. :)
                Console.WriteLine("Cancelled order " + Data.OrderId + " because of missing payment.");
                MarkAsComplete();
            }
        }
    }

    public class OrderNotPaidTimeout
    {
    }
}
