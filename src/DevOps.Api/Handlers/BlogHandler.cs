using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using DevOps.Api.Models;
using DevOps.Api.Service;
using Xerris.DotNet.Core;
using Xerris.DotNet.Core.Aws.Api;
using Xerris.DotNet.Core.Aws.Lambdas;
using Xerris.DotNet.Core.Extensions;

namespace DevOps.Api.Handlers
{
    public class BlogHandler: BaseHandler
    {
        private readonly IBlogService blogService;

        public BlogHandler() : this(IoC.Resolve<IBlogService>())
        {
        }

        public BlogHandler(IBlogService blogService)
        {
            this.blogService = blogService;
        }

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<APIGatewayProxyResponse> PostBlog(APIGatewayProxyRequest request)
        {
            var result = await blogService.PostBlog(request.Body.FromJson<BlogPost>());
            return result.Ok();
        }

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<APIGatewayProxyResponse> GetAllPosts(APIGatewayProxyRequest request)
        {
            var allPosts = await blogService.GetAllPosts();
            return allPosts.Ok();
        }
    }
}