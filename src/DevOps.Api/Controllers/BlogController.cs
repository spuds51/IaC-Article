using System.Threading.Tasks;
using DevOps.Api.Models;
using DevOps.Api.Service;
using Microsoft.AspNetCore.Mvc;

namespace DevOps.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService blogService;

        public BlogController(IBlogService blogService)
        {
            this.blogService = blogService;
        }

        [HttpGet("ping")]
        public async Task<ActionResult<BlogPost>> PostBlog([FromBody] BlogPost blogPost)
        {
            var result = await blogService.PostBlog(blogPost);
            return Ok(result);
        }
    }
}