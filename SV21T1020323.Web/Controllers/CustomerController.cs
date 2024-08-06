using Microsoft.AspNetCore.Mvc;
using SV21T1020323.BusinessLayers;
using SV21T1020323.DomainModels;

namespace SV21T1020323.Web.Controllers
{
    public class CustomerController : Controller
    {
        const int PAGE_SIZE = 20;

        public IActionResult Index(int page = 1, string searchValue = "")
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfCustomers(out rowCount, page, PAGE_SIZE, searchValue ?? "");

            int pageCount = 1;
            pageCount = rowCount / PAGE_SIZE;

            if (rowCount % PAGE_SIZE > 0)
                pageCount += 1;

            ViewBag.Page = page;
            ViewBag.PageCount = pageCount;
            ViewBag.RowCount = rowCount;
            ViewBag.SearchValue = searchValue;

            return View(data);
        }

        public IActionResult Create()
        {
            ViewBag.Title = "Bổ sung khách hàng";

            Customer customer = new Customer()
            {
                CustomerID = 0
            };

            return View("Edit", customer);
        }
        public IActionResult Edit(int id = 0)
        {
            ViewBag.Title = "Cập nhật thông tin khách hàng";

            Customer? customer = CommonDataService.GetCustomer(id);
            if (customer == null)
                return RedirectToAction("Index");

            return View(customer);
        }

        [HttpPost]
        public IActionResult Save(Customer? data)
        {
            //TODO: Kiểm tra dữ liệu đầu vào có hợp lệ hay không
            if (data.CustomerID == 0)
            {
                CommonDataService.AddCustomer(data);
            }
            else
            {
                CommonDataService.UpdateCustomer(data);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id = 0)
        {
            // Nếu lời gọi là POST thì thực hiện xóa
            if (Request.Method == "POST")
            {
                CommonDataService.DeleteCustomer(id);
                return RedirectToAction("Index");
            }

            //Nếu lời gọi là GET thì hiển thị khách hàng cần xóa
            var customer = CommonDataService.GetCustomer(id);
            if (customer == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.AllowDelete = !CommonDataService.IsUsedCustomer(id);  
            return View(customer);
        }
    }
}
