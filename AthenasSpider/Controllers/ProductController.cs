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
        //GET: Product Index
        public ActionResult Index()
        {
            var products = new ProductModel[] {
                new ProductModel{ ItemId = 1, Item = "Dog Collar", Price = 12 },
                new ProductModel{ ItemId = 3, Item = "Dog Leash", Price = 20 },
                new ProductModel{ ItemId = 2, Item = "Dog Hat", Price = 24 }
            };
            
            return View(products);
        }
    }
}