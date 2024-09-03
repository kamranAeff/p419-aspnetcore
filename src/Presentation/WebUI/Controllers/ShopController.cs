using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebUI.Services.Products;

namespace WebUI.Controllers
{
    public class ShopController(IProductService productService) : Controller
    {
        [AllowAnonymous]
        public async Task<IActionResult> Index(int page = 1, int size = 12)
        {
            var response = await productService.GetPagedAsync(page, size);
            return View(response.Data);
        }

        [AllowAnonymous]
        public IActionResult Details(string slug)
        {
            return Content(slug);
            return View();
        }

        public IActionResult Basket()
        {
            return View();
        }

        public IActionResult Checkout()
        {
            return View();
        }
    }
}
