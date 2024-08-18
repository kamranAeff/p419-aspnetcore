﻿using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
