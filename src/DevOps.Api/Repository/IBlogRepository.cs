using System;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using DevOps.Api.Models;
using Xerris.DotNet.Core.Aws.IoC;
using Xerris.DotNet.Core.Aws.Repositories.DynamoDb;

namespace DevOps.Api.Repository
{
    public interface IBlogRepository
    {
        Task<BlogPost> Save(BlogPost blogPost);
    }

    public class BlogRepository : BaseRepository<BlogPost>, IBlogRepository
    {
        public BlogRepository(ILazyProvider<IAmazonDynamoDB> clientProvider, string tableName) : base(clientProvider,
            tableName)
        {
        }

        public async Task<BlogPost> Save(BlogPost blogPost)
        {
            blogPost.Id = Guid.NewGuid().ToString("N");
            await SaveAsync(blogPost);
            return blogPost;
        }
    }
}