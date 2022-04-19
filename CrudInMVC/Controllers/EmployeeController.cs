using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudInMVC.Models;
using Microsoft.AspNetCore.Http;

namespace CrudInMVC.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeDAL context = new EmployeeDAL();
        public IActionResult List()
        {
            ViewBag.EmployeeList= context.GetAllEmployees();
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Create(IFormCollection fc)
        {
            Employee e = new Employee();
            e.EName = fc["name"];
            e.ESalary = Convert.ToInt32(fc["Salary"]);
            int res = context.Save(e);
            if (res == 1)
                return RedirectToAction("List");

            return View();

        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Employee emp = context.GetEmployeeByid(id);

            ViewBag.Name = emp.EName;
            ViewBag.Salary = emp.ESalary;
            ViewBag.Id = emp.EId;
            return View();
        }
        [HttpPost]
        public IActionResult Edit(IFormCollection form)
        {
            Employee emp = new Employee();
           
            emp.EName = form["name"];
            emp.ESalary = Convert.ToInt32(form["salary"]);
            emp.EId = Convert.ToInt32(form["EId"]);
            int res = context.Upate(emp);
            if (res == 1)
                return RedirectToAction("List");

            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Employee emp = context.GetEmployeeByid(id);
            ViewBag.name = emp.EName;
            ViewBag.salary = emp.ESalary;
            ViewBag.id = emp.EId;
            return View();
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int EId)
        {
            int res = context.Delete(EId);
            if (res == 1)
                return RedirectToAction("List");

            return View();
        }
    }
}
