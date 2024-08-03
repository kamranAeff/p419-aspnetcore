using Microsoft.AspNetCore.Mvc;
using Services.Categories;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(ICategoryService categoryService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await categoryService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await categoryService.GetByIdAsync(id);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCategoryRequestDto request)
        {
            var data = await categoryService.AddAsync(request);
            return CreatedAtAction(nameof(GetById), routeValues: new { id = data.Id }, value: data);
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<IActionResult> Edit(int id, [FromBody] EditCategoryDto request)
        {
            request.Id = id;
            var data = await categoryService.EditAsync(request);
            return Ok(data);
        }

        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> Remove(int id)
        {
            await categoryService.RemoveAsync(id);
            return NoContent();
        }

    }
}
