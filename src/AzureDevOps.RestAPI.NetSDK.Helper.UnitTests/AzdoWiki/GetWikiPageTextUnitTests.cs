using AzureDevOps.RestAPI.NetSDK.Helper.AzdoWiki.Extensions;

namespace AzureDevOps.RestAPI.NetSDK.Helper.UnitTests.AzdoWiki;

[TestClass]
public class GetWikiPageTextUnitTests : BaseTest
{
    [TestMethod]
    public async Task GetWikiPageContent()
    {
        var pageContent = await Connection.GetWikiPageText(ProjectName, "wiki name", "wiki page path");
    }
}