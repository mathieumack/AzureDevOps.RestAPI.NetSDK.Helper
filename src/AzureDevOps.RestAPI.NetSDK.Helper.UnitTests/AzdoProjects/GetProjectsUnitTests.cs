using AzureDevOps.RestAPI.NetSDK.Helper.AzdoProjects.Extensions;

namespace AzureDevOps.RestAPI.NetSDK.Helper.UnitTests.AzdoProjects;

[TestClass]
public class GetProjectsUnitTests : BaseTest
{
    [TestMethod]
    public async Task GetProjects()
    {
        var projects = await Connection.GetProjectList();
    }

    [TestMethod]
    public async Task GetProjectById()
    {
        var project = await Connection.GetProject(ProjectGuid);
    }

    [TestMethod]
    public async Task GetProjectByName()
    {
        var project = await Connection.GetProject(ProjectName);
    }
}
