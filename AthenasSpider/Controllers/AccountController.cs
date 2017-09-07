using System;

using System.Collections.Generic;

using System.Linq;

using System.Web;

using System.Web.Mvc;

namespace AthenasSpider.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        //Get: Register

        public ActionResult Register()
        {
            return View();
        }


        //Post: Register
        [HttpPost]
        public ActionResult Register(string username, string password)
        {
            return View();
        }



    }

}


}