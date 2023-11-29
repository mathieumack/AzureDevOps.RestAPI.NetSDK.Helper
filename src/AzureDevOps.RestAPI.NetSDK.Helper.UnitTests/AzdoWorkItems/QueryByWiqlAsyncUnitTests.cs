using AzureDevOps.RestAPI.NetSDK.Helper.AzdoWorkItems.Extensions;

namespace AzureDevOps.RestAPI.NetSDK.Helper.UnitTests.AzdoWorkItems;

[TestClass]
public class QueryByWiqlAsyncUnitTests : BaseTest
{
    [TestMethod]
    public async Task CreateWorkItemWithProperties()
    {
        var results = await Connection.GetQueryResultsByWiql(@"""
select [Id]
from WorkItems
where  [System.WorkItemType] <> 'Task'
""", false, false);

        Assert.IsNotNull(results);
    }
}
