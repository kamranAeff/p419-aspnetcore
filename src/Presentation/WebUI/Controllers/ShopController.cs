using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models.DTOs.Products;
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

        public async Task<IActionResult> Basket()
        {
            var response = await productService.BasketGetAllAsync();
            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> BasketInteract([FromBody]BasketInteractDto model)
        {
            var response = await productService.BasketInteractAsync(model);

            if ("true".Equals(Request.Headers["no-response"]))
            {
                return NoContent();
            }

            return PartialView("_Basket",response.Data);
        }

        public IActionResult Checkout()
        {
            return View();
        }
    }
}
