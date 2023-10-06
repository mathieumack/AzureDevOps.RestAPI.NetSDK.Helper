using AzureDevOps.RestAPI.NetSDK.Helper.AzdoWiki.Extensions;

namespace AzureDevOps.RestAPI.NetSDK.Helper.UnitTests.AzdoWiki;

[TestClass]
public class GetWikiUnitTests : BaseTest
{
    [TestMethod]
    public async Task GetReleases()
    {
        var wiki = await Connection.GetWiki("wiki name");
    }
}