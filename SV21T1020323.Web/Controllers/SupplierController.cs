using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SV21T1020323.BusinessLayers;
using SV21T1020323.DomainModels;

namespace SV21T1020323.Web.Controllers
{
    [Authorize]
    public class SupplierController : Controller
    {
        const int PAGE_SIZE = 5;
        public IActionResult Index(int page = 1, string searchValue = "")
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfSuppliers(out rowCount, page, PAGE_SIZE, searchValue ?? "");

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
            ViewBag.Title = "Bổ sung nhà cung cấp";

            Supplier supplier = new Supplier()
            {
                SupplierID = 0
            };

            return View("Edit", supplier);
        }
        public IActionResult Edit(int id = 0)
        {
            ViewBag.Title = "Cập nhật thông tin nhà cung cấp";

            Supplier? supplier = CommonDataService.GetSupplier(id);
            if (supplier == null)
            {
                return RedirectToAction("Index");
            }

            return View(supplier);
        }
        [HttpPost]

        public IActionResult Save(Supplier? data)
        {
            if (data.SupplierID == 0)
            {
                CommonDataService.AddSupplier(data);
            }
            else
            {
                CommonDataService.UpdateSupplier(data);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id = 0)
        {
            if (Request.Method == "POST")
            {
                CommonDataService.DeleteSupplier(id);
                return RedirectToAction("Index");
            }

            var supplier = CommonDataService.GetSupplier(id);
            if (supplier == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.AllowDelete = !CommonDataService.IsUsedSupplier(id);
            return View(supplier);
        }
    }
}
