using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace MvcMusicStore.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/

        public ViewResult Index()
        {
            Thread.Sleep(1500);
            return View();
        }

    }
}
