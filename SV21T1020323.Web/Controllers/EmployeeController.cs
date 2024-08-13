using Microsoft.AspNetCore.Mvc;
using SV21T1020323.BusinessLayers;
using SV21T1020323.DomainModels;
using System.IO;
using System.Reflection;

namespace SV21T1020323.Web.Controllers
{
    public class EmployeeController : Controller
    {
        const int PAGE_SIZE = 20;
        public IActionResult Index(int page = 1, string searchValue = "")
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfEmployees(out rowCount, page, PAGE_SIZE, searchValue ?? "");

            int pageCount = 1;
            pageCount = rowCount / PAGE_SIZE;

            if (rowCount % PAGE_SIZE > 0)
            {
                pageCount += 1;
            }

            ViewBag.Page = page;
            ViewBag.PageCount = pageCount;
            ViewBag.RowCount = rowCount;
            ViewBag.SearchValue = searchValue;

            return View(data);
        }

        public IActionResult Create()
        {
            ViewBag.Title = "Bổ sung nhân viên";

            Employee employee = new Employee()
            {
                EmployeeID = 0
            };

            return View("Edit", employee);
        }
        public IActionResult Edit(int id = 0)
        {
            ViewBag.Title = "Cập nhật thông tin nhân viên";

            Employee? employee = CommonDataService.GetEmployee(id);
            if (employee == null)
            {
                return RedirectToAction("Index");
            }

            return View(employee);
        }
        [HttpPost]

        public IActionResult Save(Employee? data)
        {
            if (data.EmployeeID == 0)
            {
                CommonDataService.AddEmployee(data);
            }
            else
            {
                CommonDataService.UpdateEmployee(data);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id = 0)
        {
            if (Request.Method == "POST")
            {
                CommonDataService.DeleteEmployee(id);
                return RedirectToAction("Index");
            }

            var employee = CommonDataService.GetEmployee(id);
            if (employee == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.AllowDelete = !CommonDataService.IsUsedEmployee(id);
            return View(employee);
        }
    }
}
