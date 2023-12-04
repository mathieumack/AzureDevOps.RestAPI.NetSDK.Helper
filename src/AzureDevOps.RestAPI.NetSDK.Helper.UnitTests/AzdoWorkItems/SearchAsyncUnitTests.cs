using AzureDevOps.RestAPI.NetSDK.Helper.AzdoWorkItems.Extensions;

namespace AzureDevOps.RestAPI.NetSDK.Helper.UnitTests.AzdoWorkItems;

[TestClass]
public class SearchAsyncUnitTests : BaseTest
{
    [TestMethod]
    public async Task SearchOnBacklogUnitTests()
    {
        var results = await Connection.SearchOnWorkItems("PRP obsol�te*", 10, 0, false);

        Assert.IsNotNull(results);
    }
}
