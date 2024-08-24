using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SV21T1020323.BusinessLayers;
using SV21T1020323.DomainModels;
using SV21T1020323.Web.Models;

namespace SV21T1020323.Web.Controllers
{
    [Authorize]
    public class ShipperController : Controller
    {
        const int PAGE_SIZE = 2;
        private const string SEARCH_CONDITION = "shipper_search";

        public IActionResult Index(int page = 1, string searchValue = "")
        {
            PaginationSearchInput? input = ApplicationContext.GetSessionData<PaginationSearchInput>(SEARCH_CONDITION);
            if (input == null)
            {
                input = new PaginationSearchInput()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    SearchValue = ""
                };
            }
            return View(input);
        }
        public IActionResult Search(PaginationSearchInput input)
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfShippers(out rowCount, input.Page, input.PageSize, input.SearchValue ?? "");
            var model = new ShipperSearchResult()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue ?? "",
                RowCount = rowCount,
                Data = data
            };
            ApplicationContext.SetSessionData(SEARCH_CONDITION, input);
            return View(model);
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
