using System;
using System.Collections.Generic;
using System.IO;
using Amazon.CDK;
using Amazon.CDK.AWS.APIGateway;
using Attribute = Amazon.CDK.AWS.DynamoDB.Attribute;
using Environment = Amazon.CDK.Environment;
using Amazon.CDK.AWS.DynamoDB;
using Amazon.CDK.AWS.Lambda;

namespace DevOps.Cdk
{
    public class BlogPostStack : Stack
    {
        internal BlogPostStack(Construct scope, PlatformConfig platformConfig, string id,
            IStackProps props = null) : base(scope, id, props)
        {
            props = CreateProps(platformConfig);

            var blogPostTable = new Table(this, "xerris-blog-post", new TableProps
            {
                TableName = platformConfig.BlogPostTableName,
                BillingMode = BillingMode.PAY_PER_REQUEST,
                PartitionKey = new Attribute {Name = "Id", Type = AttributeType.STRING},
                RemovalPolicy = RemovalPolicy.DESTROY
            });

            var lambdaPackage = Path.Combine(System.Environment.CurrentDirectory, platformConfig.LambdaPackage);

            var saveBlogPostLambda = CreateFunction("postBlog", lambdaPackage,
                "DevOps.Api::DevOps.Api.Handlers.BlogHandler::PostBlog", 45);

            var getAllBlogPostsLambda = CreateFunction("getAllPosts", lambdaPackage,
                "DevOps.Api::DevOps.Api.Handlers.BlogHandler::GetAllBlogPosts", 45);

            var getBlogPostById = CreateFunction("getById", lambdaPackage,
                "DevOps.Api::DevOps.Api.Handlers.BlogHandler::GetPostById" ,45);

            blogPostTable.AllowReadWrite(saveBlogPostLambda);
            blogPostTable.AllowRead(getAllBlogPostsLambda);
            blogPostTable.AllowRead(getBlogPostById);

            var restApi = new RestApi(this, "xerris-blog-api", new RestApiProps
            {
                Deploy = true,
                Description = "Api endpoints for the Blogging System",
                RestApiName = "xerris-blog-api"
            });

            var blogResource = restApi.Root.AddResource("blog");

            var postBlogIntegration = new LambdaIntegration(saveBlogPostLambda, new LambdaIntegrationOptions());
            blogResource.AddMethod("POST", postBlogIntegration);

            var getAllIntegration = new LambdaIntegration(getAllBlogPostsLambda, new LambdaIntegrationOptions());
            blogResource.AddMethod("GET", getAllIntegration);

            var findByIdIntegration = new LambdaIntegration(getBlogPostById, new LambdaIntegrationOptions());
            blogResource.AddResource("{id}")
                .AddMethod("GET", findByIdIntegration);
        }

        private Function CreateFunction(string name, string lambdaPackage, string handler, int timeout)
        {
            return new Function(this, name, new FunctionProps
            {
                Code = Code.FromAsset(lambdaPackage),
                Runtime = Runtime.DOTNET_CORE_3_1,
                FunctionName = name,
                Handler = handler,
                Timeout = Duration.Seconds(timeout)
            });
        }

        private static IStackProps CreateProps(PlatformConfig platformConfig)
        {
            return new StackProps
            {
                StackName = platformConfig.StackName,
                Env = new Environment
                {
                    Region = platformConfig.Region,
                    Account = platformConfig.Account
                },
                Tags = new Dictionary<string, string>
                {
                    {"purpose", "Learn about AWS CDK"},
                    {"cost-code", "1"},
                }
            };
        }
    }
}