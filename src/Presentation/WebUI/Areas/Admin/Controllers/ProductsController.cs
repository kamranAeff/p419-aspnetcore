using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebUI.Extensions;
using WebUI.Models.DTOs.Products;
using WebUI.Services.Brands;
using WebUI.Services.Categories;
using WebUI.Services.Products;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController(IProductService productService, ICategoryService categoryService,IBrandService brandService, IMapper mapper) : Controller
    {
        public async Task<IActionResult> Index(int page = 1, int size = 15)
        {
            var response = await productService.GetPagedAsync(page, size);
            return View(response.Data);
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await productService.GetByIdAsync(id);
            return View(response.Data);
        }

        public async Task<IActionResult> Create()
        {
            var categories = await categoryService.GetAllAsync();
            ViewBag.Categories = categories.Data.ToSelectList("Id", "Name");

            var brands = await brandService.GetAllAsync();
            ViewBag.Brands = brands.Data.ToSelectList("Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductAddDto model)
        {
            await productService.AddAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var categories = await categoryService.GetAllAsync();
            ViewBag.Categories = categories.Data.ToSelectList("Id", "Name");

            var brands = await brandService.GetAllAsync();
            ViewBag.Brands = brands.Data.ToSelectList("Id", "Name");

            var response = await productService.GetByIdAsync(id);
            var data = mapper.Map<ProductEditDto>(response.Data);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] ProductEditDto model)
        {
            await productService.EditAsync(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            await productService.RemoveAsync(id);
            return Json(new
            {
                error = false,
                message = "ok"
            });
        }
    }
}
