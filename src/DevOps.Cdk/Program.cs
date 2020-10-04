using Amazon.CDK;
using Xerris.DotNet.Core;

namespace DevOps.Cdk
{
    internal sealed class Program
    {
        public static void Main(string[] args)
        {
            var app = new App();
            // var platformConfig = new ApplicationConfigurationBuilder<PlatformConfig>().Build();
            var platformConfig = new PlatformConfig {Region = "us-west-2", Account = "123456"};
            new BlogPostStack(app,  platformConfig, "Xerris-DevOps-Stack");
            app.Synth();
        }
    }
}