using Amazon.CDK.AWS.DynamoDB;
using Amazon.CDK.AWS.Lambda;

namespace DevOps.Cdk
{
    public static class DynamoDbPermissionExtensions
    {
        public static void AllowReadWrite(this Table table, Function function)
        {
            table.GrantFullAccess(function);
        }
        public static void AllowRead(this Table table, Function function)
        {
            table.GrantFullAccess(function);
        }
    }
}