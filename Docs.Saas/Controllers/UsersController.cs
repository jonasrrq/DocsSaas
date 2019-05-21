using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Docs.Saas.Model.Custom;
using Docs.Saas.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Docs.Saas.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        { 
            _usersService = usersService;           
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginView login, string returnUrl = null)
        {
            if (returnUrl == "/Users/Logout" || returnUrl == null) returnUrl = "/";
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _usersService.Login(login);
                    if (result.Count != 0)
                    {
                        foreach (var item in result)
                        {
                            ModelState.AddModelError(string.Empty, item);
                        }
                    }
                    else return RedirectToLocal(returnUrl);
                }
                catch (Exception)
                {
                   
                }

            }
            return View(login);
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout(string returnUrl = null)
        {
            _usersService.Logout();
            return RedirectToLocal(returnUrl);
        }
    }
}