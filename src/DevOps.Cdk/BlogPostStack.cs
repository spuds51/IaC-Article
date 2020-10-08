using System;
using System.Collections.Generic;
using System.IO;
using Amazon.CDK;
using Amazon.CDK.AWS.Lambda;
using Environment = Amazon.CDK.Environment;

namespace DevOps.Cdk
{
    public class BlogPostStack : Stack
    {
        internal BlogPostStack(Construct scope, PlatformConfig platformConfig, string id,
            IStackProps props = null) : base(scope, id, props)
        {
            props = CreateProps(platformConfig);

            var projectRoot = Path.GetFullPath(Path.Combine(System.Environment.CurrentDirectory, @"../../../../.."));
            var lambdaZip = Path.Combine(System.Environment.CurrentDirectory, platformConfig.LambdaPackage);

            Console.WriteLine($"project Root: {projectRoot}");
            Console.WriteLine($"Lambda zip location: {lambdaZip}");
            
            var postLambda = new Function(this, "postBlog", new FunctionProps
                {
                    Code = Code.FromAsset(lambdaZip),
                    Runtime = Runtime.DOTNET_CORE_3_1,
                    Handler = "DevOps.Api::DevOps.Api"
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