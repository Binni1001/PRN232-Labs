using Microsoft.AspNetCore.Mvc;
using ContentNegotiationDemo.Models;

namespace ContentNegotiationDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json", "application/xml", "text/csv")]
    public class BlogController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var blogs = new List<Blog>();
            var blogPosts = new List<BlogPost>();

            blogPosts.Add(new BlogPost
            {
                Title = "Content negotiation in .NET Core",
                MetaDescription = "Content negotiation is one of the key features in .NET Core for handling different media types.",
                Published = true
            });

            blogs.Add(new Blog
            {
                Name = "My .NET Core Blog",
                Description = "A blog about .NET Core and related technologies.",
                BlogPosts = blogPosts
            });

            return Ok(blogs);
        }
    }
}
