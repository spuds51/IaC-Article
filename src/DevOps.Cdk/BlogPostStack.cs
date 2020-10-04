using Amazon.CDK;
using Xerris.DotNet.Core;

namespace DevOps.Cdk
{
    public class BlogPostStack : Stack
    {
        internal BlogPostStack(Construct scope, PlatformConfig platformConfig, string id,
            IStackProps props = null) : base(scope, id, props)
        {
            // The code that defines your stack goes here
        }
    }
}