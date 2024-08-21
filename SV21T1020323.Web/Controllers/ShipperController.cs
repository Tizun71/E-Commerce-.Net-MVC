using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SV21T1020323.BusinessLayers;
using SV21T1020323.DomainModels;

namespace SV21T1020323.Web.Controllers
{
    [Authorize]
    public class ShipperController : Controller
    {
        const int PAGE_SIZE = 20;

        public IActionResult Index(int page = 1, string searchValue = "")
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfShippers(out rowCount, page, PAGE_SIZE, searchValue ?? "");

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
            ViewBag.Title = "Bổ sung hãng giao hàng";

            Shipper shipper = new Shipper()
            {
                ShipperID = 0
            };

            return View("Edit", shipper);
        }
        public IActionResult Edit(int id = 0)
        {
            ViewBag.Title = "Cập nhật thông tin hãng giao hàng";

            Shipper? shipper = CommonDataService.GetShipper(id);
            if (shipper == null)
                return RedirectToAction("Index");

            return View(shipper);
        }

        [HttpPost]
        public IActionResult Save(Shipper? data)
        {
            //TODO: Kiểm tra dữ liệu đầu vào có hợp lệ hay không
            if (data.ShipperID == 0)
            {
                CommonDataService.AddShipper(data);
            }
            else
            {
                CommonDataService.UpdateShipper(data);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id = 0)
        {
            // Nếu lời gọi là POST thì thực hiện xóa
            if (Request.Method == "POST")
            {
                CommonDataService.DeleteShipper(id);
                return RedirectToAction("Index");
            }

            //Nếu lời gọi là GET thì hiển thị khách hàng cần xóa
            var shipper = CommonDataService.GetShipper(id);
            if (shipper == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.AllowDelete = !CommonDataService.IsUsedShipper(id);
            return View(shipper);
        }
    }
}
