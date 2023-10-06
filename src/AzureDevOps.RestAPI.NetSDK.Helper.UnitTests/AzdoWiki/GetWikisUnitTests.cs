using AzureDevOps.RestAPI.NetSDK.Helper.AzdoWiki.Extensions;

namespace AzureDevOps.RestAPI.NetSDK.Helper.UnitTests.AzdoWiki;

[TestClass]
public class GetWikisUnitTests : BaseTest
{
    [TestMethod]
    public async Task GetWikis()
    {
        var wikis = await Connection.GetWikis();
    }
}