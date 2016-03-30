using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MVC_CustomRoles.Models;
using MVC_CustomRoles.CustomFilters;


namespace MVC_CustomRoles.Controllers
{
    public class ProductController : Controller
    {
        SuperMarketEntities ctx;

        public ProductController()
        {
            ctx = new SuperMarketEntities(); 
        }

        // GET: Product
        public ActionResult Index()
        {
            var Products = ctx.ProductMasters.ToList();
            return View(Products);
        }

        [AuthLog(Roles = "Manager")]
        public ActionResult Create()
        {
            var Product = new ProductMaster();
            return View(Product);
        }
       

        [HttpPost]
        public ActionResult Create(ProductMaster p)
        {
            ctx.ProductMasters.Add(p);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        [AuthLog("AppPermissions")]
        public ActionResult SaleProduct()
        {
            ViewBag.Message = "This View is designed for the Sales Executive and Sales Manager to Sale Product.";
            return View();
        }

    }
}