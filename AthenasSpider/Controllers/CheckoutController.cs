using AthenasSpider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Threading.Tasks;



namespace AthenasSpider.Controllers
{
    public class CheckoutController : Controller
    {
        private AthenasSpiderDBEntities db = new AthenasSpiderDBEntities();


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        // GET: Checkout
        public ActionResult Index()
        {
            CheckoutViewModel model = new CheckoutViewModel();
            if (Request.Cookies.AllKeys.Contains("CartName"))
            {
                var basketName = Guid.Parse(Request.Cookies["CartName"].Value);
                model.CurrentBasket = db.Orders.Single(x => x.Name == basketName);
            }
            return View(model);
        }
        // POST: Checkout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(CheckoutViewModel model)
        {
            Models.Customer currentUser =
                    db.Customers.FirstOrDefault
                    (x => x.AspNetUser.UserName == User.Identity.Name);

            if (Request.Cookies.AllKeys.Contains("CartName"))
            {
                var basketName = Guid.Parse(Request.Cookies["CartName"].Value);
                model.CurrentBasket = db.Orders.Single(x => x.Name == basketName);
            }
            if (ModelState.IsValid)
            {
                string merchantId = System.Configuration.ConfigurationManager.AppSettings["Braintree.MerchantID"];
                string environment = ConfigurationManager.AppSettings["Braintree.Environment"];
                string publicKey = ConfigurationManager.AppSettings["Braintree.PublicKey"];
                string privateKey = ConfigurationManager.AppSettings["Braintree.PrivateKey"];
                Braintree.BraintreeGateway braintreeGateway = new Braintree.BraintreeGateway(environment, merchantId, publicKey, privateKey);

                Braintree.TransactionRequest request = new Braintree.TransactionRequest();
                request.Amount = model.CurrentBasket.ProdOrders.Sum(x => x.ProdVariant.Product.Price * x.Quantity);
                request.CreditCard = new Braintree.TransactionCreditCardRequest
                {
                    CardholderName = model.CreditCardHolderName,
                    CVV = model.CreditCardVerificationValue,
                    Number = model.CreditCardNumber,
                    ExpirationMonth = model.CreditCardExpirationMonth.ToString().PadLeft(2, '0'),    //This used to be a thing in braintree -- not sure if it still is!
                    ExpirationYear = model.CreditCardExpirationYear.ToString()
                };
                Braintree.Result<Braintree.Transaction> result = braintreeGateway.Transaction.Sale(request);

                if (result.Errors == null || result.Errors.DeepCount == 0)
                {
                    string transactionId = result.Target.Id;
                    //Remove the basket from the database and convert it to an order
                    model.CurrentBasket.Completed = true;
                    db.SaveChanges();
                    
                    
                    //Remove the basket cookie!
                    Response.SetCookie(new HttpCookie("CartName") { Expires = DateTime.UtcNow });
                    if (User.Identity.IsAuthenticated)
                    {
                        SendGridEmailService mail = new SendGridEmailService();


                        await mail.SendAsync(new Microsoft.AspNet.Identity.IdentityMessage
                        {
                            Destination = model.CurrentBasket.Customer.AspNetUser.Email,
                            Subject = "Order " + model.CurrentBasket.OId + " Completed",
                            Body = "Order Body Here"
                        });
                    }
                    return RedirectToAction("Index", "Receipt", new { id = model.CurrentBasket.OId });
                }
                else
                {
                    ModelState.AddModelError("CreditCardNumber", "Unable to authorize this card number");
                }

            }
            return View(model);
        }
    }
}