using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Docs.Saas.Service;
using Microsoft.AspNetCore.Mvc;

namespace Docs.Saas.Controllers
{
    public class PrimiumController : Controller
    {
        private readonly IPrimiumService _primiumService;

        public PrimiumController(IPrimiumService primiumService)
        {
            _primiumService = primiumService;
        }

        public async Task<IActionResult> Index( )
        {
            return View( await _primiumService.GetList(User.Identity.Name));
        }




        public async Task<IActionResult> Mis()
        {
            return View(await _primiumService.GetMisList(User.Identity.Name));
        }
    }
}