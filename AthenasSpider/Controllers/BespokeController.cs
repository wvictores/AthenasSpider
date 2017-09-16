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
    public class BespokeController : Controller
    {
        private AthenasSpiderDBEntities db = new AthenasSpiderDBEntities();

        // GET: BespokeOrders
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            var bespokeOrder = db.BespokeOrders.Include(b => b.Products).Include(b => b.Customers).Include(b => b.Orders);
            return View(bespokeOrder.ToList());
        }

        //GET: BespokeOrders/Details/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BespokeOrders bespokeOrder = db.BespokeOrders.Find(id);
            if (bespokeOrder == null)
            {
                return HttpNotFound();
            }
            return View(bespokeOrder);
        }

        // GET: BespokeOrders/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.ItemId = new SelectList(db.Products, "ItemId", "ItemName");
            ViewBag.CId = new SelectList(db.Customers, "CID", "FirstName");
            ViewBag.OId = new SelectList(db.Orders, "OId", "ShipAddressLine1");
            return View();
        }

        // POST: BespokeOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(BespokeOrders bespokeOrder, HttpPostedFileBase image, string description)
        {
            if (ModelState.IsValid)
            {
                    //Basket name is going to be a GUID
                    //e.g. FAD1F8B0-F3F1-40B1-B552-109AD15F1649
                    //Guid orderName = Guid.Parse(Request.Cookies["CartName"].Value);
                    //This line finds a basket with the matching GUID.
                    //If none exists, or it is a mismatch, this will throw an error
                    //var order = db.Orders.Single(x => x.Name == orderName);
                Order o = new Order();
                Models.Customer c = db.AspNetUsers.Single(x => x.UserName == User.Identity.Name).Customers.FirstOrDefault();
                if (c != null)

                   {
                    o.CId = c.CID;
                o.Name = Guid.NewGuid();
                 }
                o.BespokeOrders.Add(bespokeOrder);

                {

                    string folder = Server.MapPath("~/images");
                    System.IO.Directory.CreateDirectory(folder);
                    string url = Server.MapPath("~/images/" + image.FileName);

                    int i = 1;
                    while (System.IO.File.Exists(url))
                    {
                        string temp = System.IO.Path.GetFileNameWithoutExtension(url) + i + System.IO.Path.GetExtension(url);
                        url = System.IO.Path.Combine(folder, temp);
                        i++;
                    }
                    System.IO.Path.GetExtension(url);
                    image.SaveAs(url);
                    bespokeOrder.CustomerImage = "/images/" + System.IO.Path.GetFileName(url);

                    //image.SaveAs(image.FileName);
                    image.SaveAs(url);
                    //bespokeOrder.CustomerImage = "/images/" + System.IO.Path.GetFileName(url);
                    //bespokeOrder.CustomerImage = image.FileName;
                    bespokeOrder.Description = description;
                    //bespokeOrder.
                    //order.BespokeOrders.Add(image);

                }
                db.Orders.Add(o);
                //db.BespokeOrders.Add(image);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ItemId = new SelectList(db.Products, "ItemId", "ItemName", bespokeOrder.ItemId);
            ViewBag.CId = new SelectList(db.Customers, "CID", "FirstName", bespokeOrder.CId);
            ViewBag.OId = new SelectList(db.Orders, "OId", "ShipAddressLine1", bespokeOrder.OId);
            return View(bespokeOrder);
        }

        // GET: BespokeOrders/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BespokeOrders bespokeOrder = db.BespokeOrders.Find(id);
            if (bespokeOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemId = new SelectList(db.Products, "ItemId", "ItemName", bespokeOrder.ItemId);
            ViewBag.CId = new SelectList(db.Customers, "CID", "FirstName", bespokeOrder.CId);
            ViewBag.OId = new SelectList(db.Orders, "OId", "ShipAddressLine1", bespokeOrder.OId);
            return View(bespokeOrder);
        }

        // POST: BespokeOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "BespokeId,CId,OId,ItemId,VarId,DueDate,Price,Status")] BespokeOrders bespokeOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bespokeOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemId = new SelectList(db.Products, "ItemId", "ItemName", bespokeOrder.ItemId);
            ViewBag.CId = new SelectList(db.Customers, "CID", "FirstName", bespokeOrder.CId);
            ViewBag.OId = new SelectList(db.Orders, "OId", "ShipAddressLine1", bespokeOrder.OId);
            return View(bespokeOrder);
        }

        // GET: BespokeOrders/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BespokeOrders bespokeOrder = db.BespokeOrders.Find(id);
            if (bespokeOrder == null)
            {
                return HttpNotFound();
            }
            return View(bespokeOrder);
        }

        // POST: BespokeOrders/Delete/5
        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BespokeOrders bespokeOrder = db.BespokeOrders.Find(id);
            db.BespokeOrders.Remove(bespokeOrder);
            db.SaveChanges();
            return RedirectToAction("Index");
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
