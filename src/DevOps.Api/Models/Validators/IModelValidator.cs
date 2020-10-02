using System;
using Xerris.DotNet.Core.Validations;

namespace DevOps.Api.Models.Validators
{
    public interface IModelValidator
    {
        void ValidateBlogPost(BlogPost blogPost);
    }

    public class ModelValidator : IModelValidator
    {
        public void ValidateBlogPost(BlogPost blogPost)
        {
            Validate.Begin()
                .IsNotNull(blogPost, "blogPost is required").Check()
                .IsNotEmpty(blogPost.Author, nameof(blogPost.Author))
                .IsNotEmpty(blogPost.Author, nameof(blogPost.Article))
                .IsNotEmpty(blogPost.Id, nameof(blogPost.Id))
                .GreaterThan(blogPost.PostedDate, DateTime.MinValue, "Has PostedDate")
                .Check();
        }
    }
}