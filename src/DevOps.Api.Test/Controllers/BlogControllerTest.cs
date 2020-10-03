using DevOps.Api.Controllers;
using DevOps.Api.Models;
using DevOps.Api.Service;
using Moq;
using Xerris.DotNet.TestAutomation;
using Xerris.DotNet.TestAutomation.Factory;
using Xunit;

namespace DevOps.Api.Test.Controllers
{
    [Collection("Test Models")]
    public class BlogControllerTest : MockBase
    {
        private readonly Mock<IBlogService> blogService;
        private readonly BlogController controller;

        public BlogControllerTest()
        {
            blogService = Strict<IBlogService>();

            controller = new BlogController(blogService.Object);
        }

        [Fact]
        public void PostBLogPost()
        {
            var blogPost = FactoryGirl.Build<BlogPost>();

            blogService.Setup(x => x.PostBlog(blogPost))
                       .ReturnsAsync(blogPost);

            controller.PostBlog(blogPost);
        }
    }
}