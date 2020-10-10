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

            var lambdaZip = Path.Combine(System.Environment.CurrentDirectory, platformConfig.LambdaPackage);

            Console.WriteLine($"Lambda zip location: {lambdaZip}");

            var blogPostTable = new Table(this, "xerris-blog-post", new TableProps
            {
                TableName = platformConfig.BlogPostTableName,
                BillingMode = BillingMode.PAY_PER_REQUEST,
                PartitionKey = new Attribute {Name = "Id", Type = AttributeType.STRING},
                RemovalPolicy = RemovalPolicy.DESTROY
            });

            var postLambda = new Function(this, "postBlog", new FunctionProps
            {
                Code = Code.FromAsset(lambdaZip),
                Runtime = Runtime.DOTNET_CORE_3_1,
                FunctionName = "postBlog",
                Handler = "DevOps.Api::DevOps.Api.Handlers.BlogHandler::PostBlog",
                Timeout = Duration.Seconds(60)
            });

            var getAllPostsLambda = new Function(this, "getAllPosts", new FunctionProps
            {
                Code = Code.FromAsset(lambdaZip),
                Runtime = Runtime.DOTNET_CORE_3_1,
                FunctionName = "getAllPosts",
                Handler = "DevOps.Api::DevOps.Api.Handlers.BlogHandler::GetAllBlogPosts",
                Timeout = Duration.Seconds(60)
            });

            var getBlogPostById = new Function(this, "getById", new FunctionProps
            {
                Code = Code.FromAsset(lambdaZip),
                Runtime = Runtime.DOTNET_CORE_3_1,
                FunctionName = "getById",
                Handler = "DevOps.Api::DevOps.Api.Handlers.BlogHandler::GetPostById",
                Timeout = Duration.Seconds(60)
            });

            blogPostTable.AllowReadWrite(postLambda);
            blogPostTable.AllowRead(getAllPostsLambda);
            blogPostTable.AllowRead(getBlogPostById);

            var restApi = new RestApi(this, "xerris-blog-api", new RestApiProps
            {
                Deploy = true,
                Description = "Api endpoints for the Blogging System",
                RestApiName = "xerris-blog-api"
            });

            var blogResource = restApi.Root.AddResource("blog");

            var postBlogIntegration = new LambdaIntegration(postLambda, new LambdaIntegrationOptions());
            blogResource.AddMethod("POST", postBlogIntegration);

            var getAllIntegration = new LambdaIntegration(getAllPostsLambda, new LambdaIntegrationOptions());
            blogResource.AddMethod("GET", getAllIntegration);

            var findByIdIntegration = new LambdaIntegration(getBlogPostById, new LambdaIntegrationOptions());
            blogResource.AddResource("{id}")
                .AddMethod("GET", findByIdIntegration);
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