using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevOps.Api.Models;
using DevOps.Api.Models.Validators;
using DevOps.Api.Repository;

namespace DevOps.Api.Service
{
    public interface IBlogService
    {
        Task<BlogPost> PostBlog(BlogPost blogPost);
        Task<IEnumerable<BlogPost>> GetAllPosts();
    }

    public class BlogService : IBlogService
    {
        private readonly IBlogRepository repository;
        private readonly IModelValidator validator;

        public BlogService(IBlogRepository repository, IModelValidator validator)
        {
            this.repository = repository;
            this.validator = validator;
        }

        public async Task<BlogPost> PostBlog(BlogPost blogPost)
        { 
            validator.ValidateBlogPost(blogPost);
            var saved = await repository.Save(blogPost);
            return saved;
        }

        public async Task<IEnumerable<BlogPost>> GetAllPosts()
        {
            var allPosts = await repository.GetAllPosts();
            return allPosts;
        }
    }
}