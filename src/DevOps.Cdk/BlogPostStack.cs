using System;
using System.Collections.Generic;
using System.IO;
using Amazon.CDK;
using Amazon.CDK.AWS.S3;
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
            
            var bucket = new Bucket(this, "xerris-dev-ops-bucket", new BucketProps
            {
                Versioned = false,
                BucketName = "xerris-dev-ops-bucket",
                PublicReadAccess = false,
                RemovalPolicy = RemovalPolicy.DESTROY
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