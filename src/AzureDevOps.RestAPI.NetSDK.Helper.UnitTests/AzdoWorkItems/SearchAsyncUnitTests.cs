using AzureDevOps.RestAPI.NetSDK.Helper.AzdoWorkItems.Extensions;

namespace AzureDevOps.RestAPI.NetSDK.Helper.UnitTests.AzdoWorkItems;

[TestClass]
public class SearchAsyncUnitTests : BaseTest
{
    [TestMethod]
    public async Task CreateWorkItem()
    {
        var results = await Connection.Search("conversation*", 20, 0, false);

        Assert.IsNotNull(results);
    }
}
