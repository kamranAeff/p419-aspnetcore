using Microsoft.AspNetCore.Mvc;
using WebUI.Services.Products;

namespace WebUI.ViewComponents
{
    public class SaleOffProductsViewComponent(IProductService productService) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await productService.GetAllAsync();

            return View(response.Data);
        }
    }
}
