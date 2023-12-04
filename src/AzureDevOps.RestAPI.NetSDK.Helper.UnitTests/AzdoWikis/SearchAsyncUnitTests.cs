using AzureDevOps.RestAPI.NetSDK.Helper.AzdoWikis.Extensions;

namespace AzureDevOps.RestAPI.NetSDK.Helper.UnitTests.AzdoWikis;

[TestClass]
public class SearchAsyncUnitTests : BaseTest
{
    [TestMethod]
    public async Task SearchOnWikiUnitTests()
    {
        var results = await Connection.SearchOnWikis("functions*", 10, 0, false);

        Assert.IsNotNull(results);
    }
}
