using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

namespace ShoppingCartService.Commands
{
    public class AddToShoppingCartCommand: ICommand 
    {
        public string ShoppingCartId { get; set; }
        public string  AlbumId { get; set; }
    }
}
