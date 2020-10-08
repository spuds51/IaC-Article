using System;
using System.Collections.Generic;
using System.IO;
using Amazon.CDK;
using Amazon.CDK.AWS.APIGateway;
using Attribute = Amazon.CDK.AWS.DynamoDB.Attribute;
using Environment = Amazon.CDK.Environment;
using Amazon.CDK.AWS.DynamoDB;
using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AWS.S3;

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
            
            var table = new Table(this, "xerris-blog-post", new TableProps
            {
                TableName  = platformConfig.BlogPostTableName,
                BillingMode = BillingMode.PAY_PER_REQUEST,
                PartitionKey = new Attribute { Name = "Id", Type = AttributeType.STRING },
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

            table.GrantFullAccess(postLambda);
            
            var restApi = new RestApi(this, "xerris-blog-api", new RestApiProps
            {
                Deploy = true,
                Description = "Api endpoints for the Blogging System",
                RestApiName = "xerris-blog-api"
            });

            var postBlogIntegration = new LambdaIntegration(postLambda, new LambdaIntegrationOptions());
            restApi.Root.AddResource("blog")
                .AddMethod("POST", postBlogIntegration);

            var bucket = new Bucket(this, "xerris-dev-ops-bucket", new BucketProps
            {
                BucketName = "xerris-dev-ops-bucket",
                RemovalPolicy = RemovalPolicy.DESTROY,
                Versioned = false
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