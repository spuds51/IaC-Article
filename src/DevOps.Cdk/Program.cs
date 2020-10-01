﻿using Amazon.CDK;

namespace DevOps.Cdk
{
    sealed class Program
    {
        public static void Main(string[] args)
        {
            var app = new App();
            new CdkStack(app, "CdkStack");
            app.Synth();
        }
    }
}
