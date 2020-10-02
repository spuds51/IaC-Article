using System;
using System.Threading.Tasks;
using DevOps.Api.Models;

namespace DevOps.Api.Service
{
    public interface IBlogService
    {
        Task<BlogPost> PostBlog();
    }

    public class BlogService : IBlogService
    {
        public Task<BlogPost> PostBlog()
        {
            throw new NotImplementedException();
        }
    }
}