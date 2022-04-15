using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudInMVC.Models;
using Microsoft.AspNetCore.Http;

namespace CrudInMVC.Controllers
{
    public class ProductController : Controller
    {
        ProductDAL context = new ProductDAL();
        public IActionResult List()
        {
            ViewBag.ProductList = context.GetAllProducts();
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Create(IFormCollection fc)
        {
            Product p = new Product();
            p.Name = fc["name"];
            p.Price = Convert.ToDecimal(fc["price"]);
            int res = context.Save(p);
            if (res == 1)
                return RedirectToAction("List");

            return View();

        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Product prod = context.GetProductByid(id);
            ViewBag.name = prod.Name;
            ViewBag.price = prod.Price;
            ViewBag.id = prod.Id;
            return View();
        }
        [HttpPost]
        public IActionResult Edit(IFormCollection form)
        {
            Product prod = new Product();
            prod.Name = form["name"];
            prod.Price = Convert.ToDecimal(form["price"]);
            prod.Id = Convert.ToInt32(form["id"]);
            int res = context.Upate(prod);
            if (res == 1)
                return RedirectToAction("List");

            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Product prod = context.GetProductByid(id);
            ViewBag.name = prod.Name;
            ViewBag.price = prod.Price;
            ViewBag.id = prod.Id;
            return View();
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            int res = context.Delete(id);
            if (res == 1)
                return RedirectToAction("List");

            return View();
        }


    }
}

