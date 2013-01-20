using System;
using NServiceBus;
using ShoppingCartService.Commands;

namespace ShoppingCartService
{
    public class CheckOutCartMessageHandler : IHandleMessages<CheckoutShoppingCartCommand>
    {
        public void Handle(CheckoutShoppingCartCommand message)
        {
            var m = string.Format("Starting to check out cart {0}", message.ShoppingCartId);
            Console.WriteLine(m);
        }
    }
}