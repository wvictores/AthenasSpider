using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AthenasSpider.Models;

namespace AthenasSpider.Controllers
{
    public class ProductController : Controller
    {
        AthenasSpiderDBEntities db = new AthenasSpiderDBEntities();
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //GET: Product Index
        public ActionResult Index()
        {
            return View(db.Products);
        }

        // GET: Details
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return HttpNotFound("Product not found");
            }
            return View(db.Products.Find(id));
        }

        // POST: Details
        [HttpPost]
        public ActionResult Details(int id, string color, string size, int quantity)
        {
            if (ModelState.IsValid)
            {
                Guid orderName = new Guid();
                Models.Order order = null;
                //Check to see if there is a cookie already - this means the user has
                //an existing cart.  Cookie name is up to you!
                if (Request.Cookies.AllKeys.Contains("CartName"))
                {
                    //Basket name is going to be a GUID
                    //e.g. FAD1F8B0-F3F1-40B1-B552-109AD15F1649
                    orderName = Guid.Parse(Request.Cookies["CartName"].Value);
                    //This line finds a basket with the matching GUID.
                    //If none exists, or it is a mismatch, this will throw an error
                    order = db.Orders.Single(x => x.Name == orderName);
                }
                else
                {
                    //If a user doesn't have a basket yet, let's generate a GUID
                    //to identify the basket.
                    orderName = Guid.NewGuid();
                    //I'll create a new basket object and assign the name.
                    order = new Models.Order();
                    order.Name = orderName;
                    order.ShipAddressLine1 = string.Empty;
                    order.ShipAddressLine2 = string.Empty;
                    order.City = string.Empty;
                    order.State = string.Empty;
                    order.ZipCode = string.Empty;
                    //This will queue up the basket for insertion into the database
                    //It won't actually get added until I "save" the db.
                    db.Orders.Add(order);
                    //Finally, I add a cookie to the "response" object.
                    //Every subsequent request to this site should include this cookie 
                    //until it expires (in one month).  The cookie is saved on 
                    //the end-user's computer
                    Response.SetCookie(new HttpCookie("CartName", orderName.ToString())
                    {
                        Expires = DateTime.UtcNow.AddMonths(1)
                    });
                }


                //Check if I've already added this type of product to my cart
                var prodOrder = order.ProdOrders
                    .FirstOrDefault(x => x.ProdVariant.Color == color
                    && x.ProdVariant.Size == size
                    && x.ProdVariant.ItemId == id);

                //If I haven't added this type of item to the basket already, run this statement:
                if (prodOrder == null)
                {
                    //Create the new basket line item.
                    prodOrder = new Models.ProdOrder();
                    //Find the variant ID that matches the color/size requested.

                    var variant = db.ProdVariants
                        .Single(x => x.Product.ItemId == id
                        && x.Size == size
                        && x.Color == color);
                    prodOrder.VarID = variant.VarId;
                    //Add the new basket line item to the basket.
                    order.ProdOrders.Add(prodOrder);
                }
                //Add the quantity selected
                prodOrder.Quantity += quantity;
                //Finally, call Save to have Entity Framework run the insert statements
                db.SaveChanges();
                return RedirectToAction("Index", "Cart");
            }
            return View(db.Products.Find(id));
        }
    }
}