using AzureDevOps.RestAPI.NetSDK.Helper.AzdoWiki.Extensions;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;

namespace AzureDevOps.RestAPI.NetSDK.Helper.UnitTests.AzdoWiki
{
    [TestClass]
    public class GetWikiPageTextUnitTests
    {
        [TestMethod]
        public async Task GetWikiPageContent()
        {
            var connection = new VssConnection(
                                    new Uri("xxx"),
                                    new VssBasicCredential(string.Empty, "xxx"));

            var pageContent = await connection.GetWikiPageText("project name or guid", "wiki name", "wiki page path");
        }
    }
}