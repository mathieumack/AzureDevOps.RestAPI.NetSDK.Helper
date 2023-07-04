using AzureDevOps.RestAPI.NetSDK.Helper.AzdoReleases.Extensions;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;

namespace AzureDevOps.RestAPI.NetSDK.Helper.UnitTests.AzdoReleases
{
    [TestClass]
    public class GetWikiUnitTests
    {
        [TestMethod]
        public async Task GetWiki()
        {
            var connection = new VssConnection(
                                    new Uri("xxx"),
                                    new VssBasicCredential(string.Empty, "xxx"));

            var releases = await connection.GetReleases(true, false, false);
        }
    }
}
