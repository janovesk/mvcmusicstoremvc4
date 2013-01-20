using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

namespace ShoppingCartService.Commands
{
    public class CheckoutShoppingCartCommand: ICommand 
    {
        public int ShoppingCartId { get; set; }      
    }
}
