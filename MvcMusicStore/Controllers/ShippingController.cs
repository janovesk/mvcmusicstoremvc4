using System;
using System.Linq;
using System.Web.Mvc;
using MvcMusicStore.Models;

namespace MvcMusicStore.Controllers
{
    [Authorize]
    public class ShippingController : Controller
    {
        // GET: /Shipping

        public ActionResult Index(string orderId, string amount)
        {
            ViewBag.OrderId = orderId;
            ViewBag.Amount = amount;
            return View();
        }

        //  
        // POST: /Shipping/Address
        [HttpPost]
        public ActionResult Address(FormCollection values)
        {
            var shippingAddress = new ShippingAddress();
            TryUpdateModel(shippingAddress);

            var orderId = values["OrderId"];
            var amount = values["Amount"];
            //send to service with NSB

            return RedirectToAction("Index", "Payment", new {orderId, amount });

        }
    }
}
