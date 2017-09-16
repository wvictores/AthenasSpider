using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AthenasSpider.Models;

namespace AthenasSpider.Controllers
{
    public class CartController : Controller
    {
        private AthenasSpiderDBEntities db = new AthenasSpiderDBEntities();

        // GET: Cart
        public ActionResult Index()
        {
            if (Request.Cookies.AllKeys.Contains("CartName"))
            {
                //Basket name is going to be a GUID
                //e.g. FAD1F8B0-F3F1-40B1-B552-109AD15F1649
                Guid orderName = Guid.Parse(Request.Cookies["CartName"].Value);
                //This line finds a basket with the matching GUID.
                //If none exists, or it is a mismatch, this will throw an error
                var order = db.Orders.Single(x => x.Name == orderName);

                var prodOrders = order.ProdOrders;
                return View(prodOrders);
            }
            return RedirectToAction("Index", "Home");
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
