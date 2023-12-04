using AzureDevOps.RestAPI.NetSDK.Helper.AzdoWikis.Extensions;

namespace AzureDevOps.RestAPI.NetSDK.Helper.UnitTests.AzdoWikis;

[TestClass]
public class GetWikiPagesUnitTests : BaseTest
{
    [TestMethod]
    public async Task GetWikiPages()
    {
        var wikiPages = await Connection.GetWikiPages("Pathfinder", "Pathfinder.wiki");
    }
}