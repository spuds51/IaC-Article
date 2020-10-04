using Amazon.Extensions.NETCore.Setup;
using Xerris.DotNet.Core;

namespace DevOps.Cdk
{
    public class PlatformConfig : IApplicationConfigBase
    {
        public AWSOptions AwsOptions { get; set; }
        public string Environment { get; set; }
        public string Account { get; set; }
        public string Region { get; set; }
        public string StackName => $"DevOps-{Environment}";
    }
}