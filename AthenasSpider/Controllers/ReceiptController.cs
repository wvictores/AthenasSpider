using AthenasSpider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AthenasSpider.Controllers
{
    public class ReceiptController : Controller





    { 
        AthenasSpiderDBEntities db = new AthenasSpiderDBEntities();
        // GET: Receipt
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        // GET: Receipt
        public ActionResult Index(int id)
        {
            
            return View(db.Orders.Single(x => x.OId== id));
        }
    }
};
        