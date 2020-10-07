using Amazon.CDK;
using Xerris.DotNet.Core;

namespace DevOps.Cdk
{
    internal sealed class Program
    {
        public static void Main(string[] args)
        {
            var app = new App();
            var platformConfig = new ApplicationConfigurationBuilder<PlatformConfig>().Build();
            new BlogPostStack(app,  platformConfig, "Xerris-DevOps-Stack");
            app.Synth();
        }
    }
}