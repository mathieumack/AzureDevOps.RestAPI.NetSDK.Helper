using AzureDevOps.RestAPI.NetSDK.Helper.AzdoWiki.Extensions;

namespace AzureDevOps.RestAPI.NetSDK.Helper.UnitTests.AzdoWiki;

[TestClass]
public class GetWikiPagesUnitTests : BaseTest
{
    [TestMethod]
    public async Task GetWikiPages()
    {
        var wikiPages = await Connection.GetWikiPages(ProjectName, "wiki name");
    }
}