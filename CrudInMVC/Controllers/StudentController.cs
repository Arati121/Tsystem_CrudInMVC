using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudInMVC.Models;
using Microsoft.AspNetCore.Http;

namespace CrudInMVC.Controllers
{
    public class StudentController : Controller
    {
        Student1DAL context = new Student1DAL();
        public IActionResult List()
        {
            ViewBag.Student1List = context.GetAllStudent1s();
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Create(IFormCollection fc)
        {
            Student1 s = new Student1();
            s.Name = fc["Name"];
            s.Percentage = Convert.ToDecimal(fc["Percentage"]);
            int res = context.Save(s);
            if (res == 1)
                return RedirectToAction("List");

            return View();

        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            Student1 stud = context.GetStudent1Byid(Id);
            ViewBag.name = stud.Name;
            ViewBag.percentage = stud.Percentage;
            ViewBag.id = stud.Id;
            return View();
        }
        [HttpPost]
        public IActionResult Edit(IFormCollection form)
        {
            Student1 s = new Student1();
            s.Name = form["Name"];
            s.Percentage = Convert.ToDecimal(form["Percentage"]);
            s.Id = Convert.ToInt32(form["Id"]);
            int res = context.Upate(s);
            if (res == 1)
                return RedirectToAction("List");

            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Student1 stud = context.GetStudent1Byid(id);
            ViewBag.name = stud.Name;
            ViewBag.percentage = stud.Percentage;
            ViewBag.id = stud.Id;
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
