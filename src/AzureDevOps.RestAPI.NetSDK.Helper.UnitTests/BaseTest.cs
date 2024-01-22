using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;

namespace AzureDevOps.RestAPI.NetSDK.Helper.UnitTests
{
    public abstract class BaseTest
    {
        protected VssConnection Connection { get; private set; }

        protected string ProjectGuid { get; } = "<set default project guid here for your tests>";

        protected string ProjectName { get; } = "<set default project name here for your tests>";

        public BaseTest()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.development.json", optional: true)
                .Build();

            var settings = new AzdoOptions();
            config.GetRequiredSection("AzureDevopsConnection")
                .Bind(settings, options => options.ErrorOnUnknownConfiguration = true);

            Connection = new VssConnection(
                                        new Uri(settings.Uri),
                                        new VssBasicCredential(string.Empty, settings.Pat));
        }
    }
}
