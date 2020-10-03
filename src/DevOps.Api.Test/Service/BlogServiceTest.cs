using DevOps.Api.Models;
using DevOps.Api.Models.Validators;
using DevOps.Api.Repository;
using DevOps.Api.Service;
using Moq;
using Xerris.DotNet.TestAutomation;
using Xerris.DotNet.TestAutomation.Factory;
using Xunit;

namespace DevOps.Api.Test.Service
{
    [Collection("Test Models")]
    public class BlogServiceTest : MockBase
    {
        private Mock<IBlogRepository> repository;
        private Mock<IModelValidator> validator;
        private BlogService service;

        public BlogServiceTest()
        {
            repository = Strict<IBlogRepository>();
            validator = Strict<IModelValidator>();
            service = new BlogService(repository.Object, validator.Object);
        }

        [Fact]
        public void SavePost()
        {
            var blogPost = FactoryGirl.Build<BlogPost>();

            validator.Setup(x => x.ValidateBlogPost(blogPost));
            repository.Setup(x => x.Save(blogPost))
                      .ReturnsAsync(blogPost);

            service.PostBlog(blogPost);
        }
    }
}