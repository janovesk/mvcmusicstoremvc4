using System;
using NServiceBus;
using ShoppingCartService.Commands;

namespace ShoppingCartService
{
    public class AddToCartMessageHandler : IHandleMessages<AddToShoppingCartCommand>
    {
        public void Handle(AddToShoppingCartCommand message)
        {
            var m = string.Format("Added album {0} to cart {1}", message.AlbumId, message.ShoppingCartId);
            Console.WriteLine(m);
        }
    }
}