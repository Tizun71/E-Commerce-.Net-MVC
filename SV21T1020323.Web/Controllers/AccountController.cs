using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SV21T1020323.BusinessLayers;
using SV21T1020323.Web;
using SV21T1020323.Web.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SV21T1020323.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {            
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username = "", string password = "")
        {
            ViewBag.UserName = username;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError("Error", "Nhập tên và mật khẩu!");
                return View();
            }

            var userAccount = UserAccountService.Authorize(username, password);

            if (userAccount == null)
            {
                ModelState.AddModelError("Error", "Đăng nhập thất bại!");
                return View();
            }

            //Đăng nhập thành công, tạo dữ liệu để lưu thông tin đăng nhập
            var userData = new WebUserData()
            {
                UserId = userAccount.UserID,
                UserName = userAccount.UserName,
                DisplayName = userAccount.FullName,
                Email = userAccount.Email,
                Photo = userAccount.Photo,
                ClientIP = HttpContext.Connection.RemoteIpAddress?.ToString(),
                SessionId = HttpContext.Session.Id,
                AdditionalData = "",
                Roles = userAccount.RoleNames.Split(',').ToList()
            };
            //Thiết lập phiên đăng nhập cho tài khoản
            await HttpContext.SignInAsync(userData.CreatePrincipal());
            //Redirec về trang chủ sau khi đăng nhập
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult AccessDenined()
        {
            return View();
        }
        public IActionResult ChangePassword(ChangePasswordViewModel data)
        {
            return View(data);
        }

        public IActionResult Save(string userName, ChangePasswordViewModel data)
        {
            //Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(data.oldPassword))
                ModelState.AddModelError(nameof(data.oldPassword), "Mật khẩu không được để trống");
            if (string.IsNullOrWhiteSpace(data.oldPassword))
                ModelState.AddModelError(nameof(data.newPassword), "Mật khẩu mới không được để trống");
            if (data.newPassword != data.confirmNewPassword)
                ModelState.AddModelError(nameof(data.confirmNewPassword), "Mật khẩu xác nhận không khớp");
            if (data.newPassword == data.oldPassword)
                ModelState.AddModelError(nameof(data.newPassword), "Mật khẩu mới không được trùng với mật khẩu hiện tại");
            if (!UserAccountService.IsOldPassword(userName, data.oldPassword))
                ModelState.AddModelError(nameof(data.oldPassword), "Mật khẩu không đúng");

            if (!ModelState.IsValid)
                return View("ChangePassword", data);

            UserAccountService.ChangePassword(userName, data.oldPassword, data.newPassword);
            return RedirectToAction("Index", "Home");
        }
    }
}
