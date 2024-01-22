using AzureDevOps.RestAPI.NetSDK.Helper.AzdoReleases.Extensions;

namespace AzureDevOps.RestAPI.NetSDK.Helper.UnitTests.AzdoReleases;

[TestClass]
public class GetWikiUnitTests : BaseTest
{
    [TestMethod]
    public async Task GetWiki()
    {
        var releases = await Connection.GetReleases(true, false, false);
    }
}
