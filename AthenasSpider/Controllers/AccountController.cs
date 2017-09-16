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

        Models.AthenasSpiderDBEntities db = new Models.AthenasSpiderDBEntities();

        //Type override and hit tab
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        // GET: Account/SignIn
        public ActionResult SignIn()
        {
            return View();
        }

        // POST: Account/SignIn
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(string username, string password, bool? rememberMe, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<IdentityUser>>();
                IdentityUser user = userManager.FindByName(username);
                if (user != null)
                {
                    if (userManager.CheckPassword(user, password))
                    {
                        var userIdentity = userManager.CreateIdentity(user,
                            DefaultAuthenticationTypes.ApplicationCookie);

                        var authenticationManager = HttpContext.GetOwinContext().Authentication;
                        authenticationManager.SignIn(
                            new Microsoft.Owin.Security.AuthenticationProperties
                            {
                                IsPersistent = rememberMe ?? false,
                                ExpiresUtc = DateTime.UtcNow.AddDays(7)
                            },
                            userIdentity);
                        if (!string.IsNullOrEmpty(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }

                ViewBag.Errors = new string[] { "Could not sign in with this username / password" };


            }
            return View();
        }

        public ActionResult SignOut()
        {
            var authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(string email)
        {
            if (ModelState.IsValid)
            {
                var manager = HttpContext.GetOwinContext().GetUserManager<UserManager<IdentityUser>>();
                IdentityUser user = manager.FindByEmail(email) ?? manager.FindByName(email);
                if(user != null)
                {
                    string resetToken = manager.GeneratePasswordResetToken(user.Id);
                    string body = string.Format(
                        "<a href=\"{0}/account/resetpassword?email={1}&token={2}\">Reset your password</a>",
                        Request.Url.GetLeftPart(UriPartial.Authority),
                        email,
                        resetToken);

                    manager.SendEmail(user.Id, "Reset Your Athena's Spider Password", body);

                }
                return RedirectToAction("ForgotPasswordSent");
            }
            return View();
        }

        public ActionResult ForgotPasswordSent()
        {
            return View();
        }

        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(string email, string token, string password)
        {
            if (ModelState.IsValid)
            {
                var manager =
                    HttpContext.GetOwinContext().GetUserManager<UserManager<IdentityUser>>();
                IdentityUser user = manager.FindByEmail(email);
                IdentityResult result =
                    manager.ResetPassword(user.Id, token, password);
                if (result.Succeeded)
                {
                    TempData["PasswordReset"] = "Your password has been reset successfully";
                    return RedirectToAction("SignIn");
                }
                ViewBag.Errors = result.Errors;
            }
            return View();

        }

        // GET: Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Register
        [HttpPost]
        public ActionResult Register(string username, string password, string firstname, string lastname, DateTime? dateofbirth)
        {
            //Include these at the top:
            //using Microsoft.AspNet.Identity;
            //using Microsoft.AspNet.Identity.EntityFramework;
            //using Microsoft.AspNet.Identity.Owin;
            var manager = HttpContext.GetOwinContext().GetUserManager<UserManager<IdentityUser>>();

            IdentityUser newUser = new IdentityUser(username);
            newUser.Email = username;

            //This can fail - 
            //password might not be complex enough, or username might already be in use
            IdentityResult result = manager.Create(newUser, password);

            if (!result.Succeeded)
            {
                ViewBag.Errors = result.Errors;
                return View();
            }

            db.Customers.Add(new Models.Customer
            {
                AspNetUserId = newUser.Id,
                FirstName = firstname,
                LastName = lastname,

            });

            db.SaveChanges();

            //If the user creation succeeded, use the code below to set the sign-in cookie:
            //TODO: Most sites require you to confirm email before letting you sign-in.

            string token = manager.GenerateEmailConfirmationToken(newUser.Id);
            string body = string.Format(
                    "<a href=\"{0}/account/confirmaccount?email={1}&token={2}\">Confirm Your Account</a>",
                    Request.Url.GetLeftPart(UriPartial.Authority),
                    username,
                    token);

            manager.SendEmail(newUser.Id, "Confirm Your Athena's Spider Account", body);

            return RedirectToAction("SignIn");
        }
        
        //[HttpPost]
        //public ActionResult ValidateAddress(string street1, string street2, string city, string state, string zip)
        //    if (ModelState.IsValid)
        //{
        //    string authId = ConfigurationManager.AppSettings["SmartyStreets.AuthID"];
        //    string authToken = ConfigurationManager.AppSettings["SmartyStreets.AuthToken"];
        //    SmartyStreets.USStreetApi.ClientBuilder builder = new SmartyStreets.USStreetApi.ClientBuilder(authId, authToken);
        //    SmartyStreets.USStreetApi.Client client = builder.Build();
        //    SmartyStreets.USStreetApi.Lookup lookup = new SmartyStreets.USStreetApi.Lookup();
        //    lookup.City = city;
        //    lookup.State = state;
        //    lookup.Street = street1;
        //    lookup.Street2 = street2;
        //    lookup.ZipCode = zip;
        //    client.Send(lookup);
        //    return Json(lookup.Result);
        }
    }
