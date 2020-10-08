using Amazon.Lambda.APIGatewayEvents;
using DevOps.Api.Handlers;
using DevOps.Api.Models;
using DevOps.Api.Service;
using Moq;
using Xerris.DotNet.Core.Extensions;
using Xerris.DotNet.TestAutomation;
using Xerris.DotNet.TestAutomation.Factory;
using Xunit;

namespace DevOps.Api.Test.Controllers
{
    [Collection("Test Models")]
    public class BlogControllerTest : MockBase
    {
        private readonly Mock<IBlogService> blogService;
        private readonly BlogHandler handler;

        public BlogControllerTest()
        {
            blogService = Strict<IBlogService>();

            handler = new BlogHandler(blogService.Object);
        }

        [Fact]
        public void PostBLogPost()
        {
            var blogPost = FactoryGirl.Build<BlogPost>();

            blogService.Setup(x => x.PostBlog(It.Is<BlogPost>(x => x.Matches(blogPost))))
                       .ReturnsAsync(blogPost);

            var request = new APIGatewayProxyRequest {Body = blogPost.ToJson()};
            
            handler.PostBlog(request);
        }
    }
}