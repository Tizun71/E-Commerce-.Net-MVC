using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SV21T1020323.BusinessLayers;
using SV21T1020323.DomainModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SV21T1020323.Web.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        const int PAGE_SIZE = 5;
        public IActionResult Index(int page = 1, string searchValue = "")
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfCategories(out rowCount, page, PAGE_SIZE, searchValue ?? "");

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
            ViewBag.Title = "Bổ sung loại hàng";

            Category category = new Category()
            {
                CategoryID = 0
            };

            return View("Edit", category);
        }
        public IActionResult Edit(int id = 0)
        {
            ViewBag.Title = "Cập nhật thông tin loại hàng";

            Category? category = CommonDataService.GetCategory(id);
            if (category == null)
            {
                return RedirectToAction("Index");
            }

            return View(category);
        }
        [HttpPost]

        public IActionResult Save(Category? data)
        {
            if (data.CategoryID == 0)
            {
                CommonDataService.AddCategory(data);
            }
            else
            {
                CommonDataService.UpdateCategory(data);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id = 0)
        {
            if (Request.Method == "POST")
            {
                CommonDataService.DeleteCategory(id);
                return RedirectToAction("Index");
            }
            
            var category = CommonDataService.GetCategory(id);
            if (category == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.AllowDelete = !CommonDataService.IsUsedCategory(id);
            return View(category);
        }
    }
}
