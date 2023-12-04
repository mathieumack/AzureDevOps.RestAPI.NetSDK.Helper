using AzureDevOps.RestAPI.NetSDK.Helper.AzdoWorkItems.Extensions;

namespace AzureDevOps.RestAPI.NetSDK.Helper.UnitTests.AzdoWorkItems;

[TestClass]
public class QueryByWiqlAsyncUnitTests : BaseTest
{
    [TestMethod]
    public async Task GetWorkItemsByWiql()
    {
        var results = await Connection.GetQueryResultsByWiql(@"""
select [Id]
from WorkItems
where  [System.WorkItemType] <> 'Task'
""", false, false, false, false);

        Assert.IsNotNull(results);
    }

    [TestMethod]
    public async Task GetWorkItemsByIds()
    {
        var ids = new List<int>()
        {
            1019,
            1004
        };

        var results = await Connection.GetQueryResultsByWiql($"SELECT [System.Id] FROM workitems WHERE [System.Id] IN ({string.Join(",", ids)})", false, false, true, false);

        var publicUri = results[1].GetPublicUri();

        Assert.IsNotNull(results);
    }
}
