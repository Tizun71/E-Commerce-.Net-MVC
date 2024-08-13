using Microsoft.AspNetCore.Mvc;
using SV21T1020323.BusinessLayers;
using SV21T1020323.DomainModels;
using System.Buffers;

namespace SV21T1020323.Web.Controllers
{
    public class ProductController : Controller
    {
        const int PAGE_SIZE = 20;
        public IActionResult Index(int page = 1, string searchValue = "", int categoryId = 0, int supplierId = 0, decimal minPrice = 0, decimal maxPrice = 0)
        {
            int rowCount = 0;
            var data = ProductDataService.ListProducts(out rowCount, page, PAGE_SIZE, searchValue ?? "", categoryId, supplierId, minPrice, maxPrice);

            Models.ProductSearchResult model = new Models.ProductSearchResult()
            { 
                Page = page,
                PageSize = PAGE_SIZE,
                RowCount = rowCount,
                SearchValue = searchValue ?? "",
                CategoryId = categoryId,
                SupplierId = supplierId,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                Data = data
            };

            return View(model);
        }

        public IActionResult Create()
        {
            ViewBag.Title = "Bổ sung sản phẩm";

            Product product = new Product()
            {
                ProductID = 0
            };

            return View("Edit", product);
        }
        public IActionResult Edit(int id = 0)
        {
            ViewBag.Title = "Cập nhật thông tin sản phẩm";

            Product? product = ProductDataService.GetProduct(id);
            if (product == null)
                return RedirectToAction("Index");

            return View(product);
        }

        [HttpPost]
        public IActionResult Save(Product? data)
        {
            ViewBag.Title = data.ProductID == 0 ? "Bổ sung sản phẩm" : "Cập nhật thông tin sản phẩm";

            if (string.IsNullOrWhiteSpace(data.ProductName))
            {
                ModelState.AddModelError(nameof(data.ProductName), "Tên sản phẩm không được để trống");
            }
            if (string.IsNullOrWhiteSpace(data.Unit))
            {
                ModelState.AddModelError(nameof(data.Unit), "Đơn vị tính không được để trống");
            }
            if (data.Price == null)
            {
                ModelState.AddModelError(nameof(data.Price), "Giá tiền không được để trống");
            }
            if (data.CategoryID <= 0)
            {
                ModelState.AddModelError(nameof(data.CategoryID), "Vui lòng chọn mã loại hàng");
            }
            if (data.SupplierID <= 0)
            {
                ModelState.AddModelError(nameof(data.SupplierID), "Vui lòng chọn nhà cung cấp");
            }

            data.ProductDescription = data.ProductDescription ?? "";
            data.Photo = data.Photo;
            data.IsSelling = data.IsSelling;

/*            if (!ModelState.IsValid)
            {
                return View("Edit", data);
            }*/

            if (data.ProductID == 0)
            {
                ProductDataService.AddProduct(data);
            }
            else
            {
                ProductDataService.UpdateProduct(data);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id = 0)
        {
            return View();
        }

        public IActionResult Photo(int id = 0, string method = "", int photoId = 0)
        {
            switch (method)
            {
                case "add":
                    ViewBag.Title = "Bổ sung ảnh cho mặt hàng";
                    return View();
                case "edit":
                    ViewBag.Title = "Thay đổi ảnh của mặt hàng";
                    return View();
                case "delete":
                    //Xóa ảnh trực tiếp, Không cần confirm
                    return RedirectToAction("Edit", new {id = id});
                default:
                    return RedirectToAction("Index");
            }
        }

        public IActionResult Attribute(int id = 0, string method = "", int attributeId = 0)
        {
            switch (method)
            {
                case "add":
                    ViewBag.Title = "Bổ sung thuộc tính cho mặt hàng";
                    return View();
                case "edit":
                    ViewBag.Title = "Thay đổi thuộc tính của mặt hàng";
                    return View();
                case "delete":
                    //Xóa ảnh trực tiếp, Không cần confirm
                    return RedirectToAction("Edit", new { id = id });
                default:
                    return RedirectToAction("Index");
            }
        }
    }
}
