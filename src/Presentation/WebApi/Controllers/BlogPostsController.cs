using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Services.BlogPosts;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController(IBlogPostService blogPostService, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await blogPostService.GetAllAsync();

            var response = mapper.Map<List<BlogPostDto>>(data);

            return Ok(response);
        }

        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await blogPostService.GetByIdAsync(id);

            var response = mapper.Map<BlogPostDto>(data, cfg =>
            {
                if (Request.Headers.TryGetValue("dateFormat", out StringValues formats))
                    cfg.Items.Add("dateFormat", formats.FirstOrDefault());

                string host = $"{Request.Scheme}://{Request.Host}";
                cfg.Items.Add("host", host);

            });
            return Ok(response);
        }
    }
}
