using System;
using System.Linq;
using System.Web.Mvc;
using MvcMusicStore.Models;

namespace MvcMusicStore.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        //
        // GET: /Payment

        public ActionResult Index()
        {
            return View();
        }

        //
        // POST: /Payment/Visa
        [HttpPost]
        public ActionResult Visa(VisaPayment visaPayment)
        {
            if (!ModelState.IsValid)
                return View("Index");
            
            var cart = ShoppingCart.GetCart(this.HttpContext);
            var orderId = 1; //cart.orderId;
            
            return RedirectToAction("Complete", new { id = orderId});

        }

        //
        // GET: /Payment/Complete

        public ActionResult Complete(int id)
        {
            return View(id);
        }
    }
}
