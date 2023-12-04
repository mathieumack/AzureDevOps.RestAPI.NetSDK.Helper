using AzureDevOps.RestAPI.NetSDK.Helper.AzdoWikis.Extensions;

namespace AzureDevOps.RestAPI.NetSDK.Helper.UnitTests.AzdoWikis;

[TestClass]
public class GetWikisUnitTests : BaseTest
{
    [TestMethod]
    public async Task GetWikis()
    {
        var wikis = await Connection.GetWikis();
    }
}