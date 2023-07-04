using AzureDevOps.RestAPI.NetSDK.Helper.AzdoWiki.Extensions;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;

namespace AzureDevOps.RestAPI.NetSDK.Helper.UnitTests.AzdoWiki
{
    [TestClass]
    public class GetWikisUnitTests
    {
        [TestMethod]
        public async Task GetWikis()
        {
            var connection = new VssConnection(
                                    new Uri("xxx"),
                                    new VssBasicCredential(string.Empty, "xxx"));

            var wikis = await connection.GetWikis();
        }
    }
}