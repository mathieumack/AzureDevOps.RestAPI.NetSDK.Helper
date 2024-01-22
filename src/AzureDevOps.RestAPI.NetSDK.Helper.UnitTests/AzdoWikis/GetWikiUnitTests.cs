using AzureDevOps.RestAPI.NetSDK.Helper.AzdoWikis.Extensions;

namespace AzureDevOps.RestAPI.NetSDK.Helper.UnitTests.AzdoWikis;

[TestClass]
public class GetWikiUnitTests : BaseTest
{
    [TestMethod]
    public async Task GetReleases()
    {
        var wiki = await Connection.GetWiki("wiki name");
    }
}