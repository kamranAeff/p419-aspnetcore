using Microsoft.AspNetCore.Mvc;
using WebUI.Services.Categories;

namespace WebUI.ViewComponents
{
    public class AllCategoriesViewComponent(ICategoryService categoryService) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string view = null)
        {
            var response = await categoryService.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(view))
                return View(view, response.Data);

            return View(response.Data);
        }
    }
}