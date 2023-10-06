using AzureDevOps.RestAPI.NetSDK.Helper.AzdoWorkItems.Extensions;

namespace AzureDevOps.RestAPI.NetSDK.Helper.UnitTests.AzdoWorkItems;

[TestClass]
public class CreateTaskUnitTests : BaseTest
{
    [TestMethod]
    public async Task CreateSimpleTask()
    {
        var createdTask = await Connection.CreateWorkItem("Task",
            ProjectName,
            $"My sample task {Guid.NewGuid()}");
    }

    [TestMethod]
    public async Task CreateWorkItemWithProperties()
    {
        var createdTask = await Connection.CreateWorkItem("Product Backlog Item",
            ProjectName,
            $"My sample WI {Guid.NewGuid()}",
            new Dictionary<string, string>()
            {
                { "System.Description", "Created by unit test from the SDK Helper" },
                { "Microsoft.VSTS.Common.AcceptanceCriteria", "It should works" }
            });
    }
}
