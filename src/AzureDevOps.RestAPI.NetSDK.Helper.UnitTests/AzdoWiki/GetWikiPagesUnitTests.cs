using AzureDevOps.RestAPI.NetSDK.Helper.AzdoWiki.Extensions;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;

namespace AzureDevOps.RestAPI.NetSDK.Helper.UnitTests.AzdoWiki
{
    [TestClass]
    public class GetWikiPagesUnitTests
    {
        [TestMethod]
        public async Task GetWikiPages()
        {
            var connection = new VssConnection(
                                    new Uri("xxx"),
                                    new VssBasicCredential(string.Empty, "xxx"));

            var wikiPages = await connection.GetWikiPages("project name or guid", "wiki name");
        }
    }
}