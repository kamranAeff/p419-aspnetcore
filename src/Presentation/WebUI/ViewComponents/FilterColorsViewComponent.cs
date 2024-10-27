using Microsoft.AspNetCore.Mvc;
using WebUI.Services.Colors;

namespace WebUI.ViewComponents
{
    public class FilterColorsViewComponent(IColorService colorService) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await colorService.GetAllAsync();
            return View(response.Data);
        }
    }
}