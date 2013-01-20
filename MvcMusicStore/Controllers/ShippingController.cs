using System;
using System.Linq;
using System.Web.Mvc;
using MvcMusicStore.Models;

namespace MvcMusicStore.Controllers
{
    [Authorize]
    public class ShippingController : Controller
    {
        MusicStoreEntities storeDB = new MusicStoreEntities();
        //
        // GET: /Checkout/Address

        public ActionResult Address()
        {
            return View();
        }

        //
        // POST: /Checkout/Address
        [HttpPost]
        public ActionResult Address(FormCollection values)
        {
            var order = new Order();
            TryUpdateModel(order);

            try
            {
                order.Username = User.Identity.Name;
                order.OrderDate = DateTime.Now;

                //Save Order
                storeDB.Orders.Add(order);
                storeDB.SaveChanges();

                //Process the order
                var cart = ShoppingCart.GetCart(this.HttpContext);
                cart.CreateOrder(order);

                return RedirectToAction("Index", "Payment", new { id = order.OrderId });

            }
            catch
            {
                //Invalid - redisplay with errors
                return View(order);
            }
        }
    }
}
