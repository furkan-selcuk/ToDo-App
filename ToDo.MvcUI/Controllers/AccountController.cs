using Microsoft.AspNetCore.Mvc;
using ToDo.MvcUI.Models;
using ToDo.MvcUI.Services;

namespace ToDo.MvcUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IApiService _apiService;

        public AccountController(IApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var token = await _apiService.LoginAsync("auth/login", model);

            if (string.IsNullOrEmpty(token))
            {
                ModelState.AddModelError("", "Kullanıcı adı veya sifre hatalı");
                return View(model);
            }

            _apiService.SetToken(token);
            return RedirectToAction("Index", "Tasks");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
