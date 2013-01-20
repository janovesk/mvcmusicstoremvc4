﻿using System;
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
            
            //send to service with NSB
            
            return RedirectToAction("Complete", new {orderId , amount});
        }

        //
        // GET: /Payment/Complete
        public ActionResult Complete(int orderId, decimal amount)
        {
            ViewBag.OrderId = orderId;
            ViewBag.Amount = amount;
            return View();
        }
    }
}
