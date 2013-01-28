using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using MvcMusicStore.Models;
using PaymentService.Commands;

namespace MvcMusicStore.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        //
        // GET: /Payment
        public ActionResult Index(string orderId, string amount)
        {
            ViewBag.OrderId = orderId;
            ViewBag.Amount = amount;
            return View();
        }

        //
        // POST: /Payment/Visa
        [HttpPost]
        public ActionResult Visa(FormCollection values)
        {
            var visaPayment = new VisaPayment();
            TryUpdateModel(visaPayment);

            if (!ModelState.IsValid)
                return View("Index");

            var orderId = values["OrderId"];
            var amount = values["Amount"];

            MvcApplication.Bus.Send<PayOrderCommand>(c =>
                                                         {
                                                             c.Name = visaPayment.Name;
                                                             c.VisaNumber = visaPayment.CreditCardNumber;
                                                             c.ExpiryMonth = Int32.Parse(visaPayment.ExpiryMonth);
                                                             c.ExpiryYear = Int32.Parse(visaPayment.ExpiryYear);
                                                             c.AmountToPay = Decimal.Parse(amount, CultureInfo.InvariantCulture);
                                                             c.OrderId = orderId;
                                                         });
            
            return RedirectToAction("Complete", new {orderId , amount});
        }

        //
        // GET: /Payment/Complete
        public ActionResult Complete(string orderId, decimal amount)
        {
            ViewBag.OrderId = orderId;
            ViewBag.Amount = amount;
            return View();
        }
    }
}
