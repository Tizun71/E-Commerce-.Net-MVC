using Microsoft.AspNetCore.Mvc;
using SV21T1020323.Web.Models;
using System.Diagnostics;

namespace SV21T1020323.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Customers() 
        {
            var model = BusinessLayers.CommonDataService.ListOfCustomers();
            return View(model);
        }
        public IActionResult Categories()
        {
            var model = BusinessLayers.CommonDataService.ListOfCategories();
            return View(model);
        }

        public IActionResult Suppliers() {
            var model = BusinessLayers.CommonDataService.ListOfSuppliers();
            return View(model);
        }

        public IActionResult Shippers()
        {
            var model = BusinessLayers.CommonDataService.ListOfShippers();
            return View(model);
        }

        public IActionResult Employees() 
        {
            var model = BusinessLayers.CommonDataService.ListOfEmployees();
            return View(model);
        }
        public IActionResult Products()
        {
            var model = BusinessLayers.CommonDataService.ListOfEmployees();
            return View(model);
        }
    }
}
