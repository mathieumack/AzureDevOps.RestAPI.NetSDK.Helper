using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;

namespace AzureDevOps.RestAPI.NetSDK.Helper.UnitTests
{
    public abstract class BaseTest
    {
        private VssConnection connection;
        protected VssConnection Connection
        {
            get
            {
                if(connection is null)
                    connection = new VssConnection(
                                        new Uri("<set organisation uri here>"),
                                        new VssBasicCredential(string.Empty, "<set pat here>"));
                return connection;
            }
        }

        protected string ProjectGuid { get; } = "<set default project guid here for your tests>";

        protected string ProjectName { get; } = "<set default project name here for your tests>";
    }
}
