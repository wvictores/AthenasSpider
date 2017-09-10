using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
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

        public ActionResult SignOut()
        {
            var authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        // GET: Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Register
        [HttpPost]
        public ActionResult Register(string username, string password)
        {
            //Include these at the top:
            //using Microsoft.AspNet.Identity;
            //using Microsoft.AspNet.Identity.EntityFramework;
            //using Microsoft.AspNet.Identity.Owin;
            var manager = HttpContext.GetOwinContext().GetUserManager<UserManager<IdentityUser>>();

            IdentityUser newUser = new IdentityUser(username);

            //This can fail - 
            //password might not be complex enough, or username might already be in use
            IdentityResult result = manager.Create(newUser, password);
            if (!result.Succeeded)
            {
                ViewBag.Errors = result.Errors;
                return View();
            }

            //If the user creation succeeded, use the code below to set the sign-in cookie:
            //TODO: Most sites require you to confirm email before letting you sign-in.

            var authenticationManager = HttpContext.GetOwinContext().Authentication;

            var userIdentity = manager.CreateIdentity(newUser,
                DefaultAuthenticationTypes.ApplicationCookie);

            authenticationManager.SignIn(
                new Microsoft.Owin.Security.AuthenticationProperties(),
                userIdentity);

            return RedirectToAction("Index", "Home");
    }
    }
}