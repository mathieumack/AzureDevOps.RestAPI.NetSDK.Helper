using AzureDevOps.RestAPI.NetSDK.Helper.AzdoWikis.Extensions;

namespace AzureDevOps.RestAPI.NetSDK.Helper.UnitTests.AzdoWikis;

[TestClass]
public class GetWikiPageTextUnitTests : BaseTest
{
    [TestMethod]
    public async Task GetWikiPageContent()
    {
        var pageContent = await Connection.GetWikiPageTextByPath("Pathfinder", "Pathfinder.wiki", "wiki page path");
    }
}