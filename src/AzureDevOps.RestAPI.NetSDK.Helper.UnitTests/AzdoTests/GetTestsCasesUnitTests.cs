using AzureDevOps.RestAPI.NetSDK.Helper.AzdoTests.Extensions;
using AzureDevOps.RestAPI.NetSDK.Helper.AzdoWorkItems.Extensions;
using AzureDevOps.RestAPI.NetSDK.Helper.AzdoTests.Domain;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;

namespace AzureDevOps.RestAPI.NetSDK.Helper.UnitTests.AzdoTests;

[TestClass]
public class GetTestsCasesUnitTests : BaseTest
{
    [TestMethod]
    public async Task GetTestCases()
    {
        var wiEntry = await Connection.GetWorkItem(1145, false, false);
        var wis = await Connection.GetWorkItemTestCases(1145);
        var steps = wis[0].GetTestSteps();

        // Try to create a Test case :
        string projectId = wis[0].WorkItem.Fields["System.TeamProject"].ToString();
        string testCaseTitle = $"New test case {Guid.NewGuid()}";

        // Build steps :
        steps newSteps = new steps();
        newSteps.step = new stepsStep[2]
        {
            new stepsStep()
            {
                id = 1,
                description = "Click on login too",
                type = "ActionStep", // No results expected
                parameterizedString = new stepsStepParameterizedString[2]
                {
                    new stepsStepParameterizedString()
                    {
                        isformatted = true,
                        Value = "<div><p>Click on login</p></div>"
                    },
                    new stepsStepParameterizedString()
                    {
                        isformatted = true,
                        Value = "<div><p><br /></div>"
                    }
                }
            },
            new stepsStep()
            {
                id = 2,
                description = "Click on login too",
                type = "ValidationStep",
                parameterizedString = new stepsStepParameterizedString[2]
                {
                    new stepsStepParameterizedString()
                    {
                        isformatted = true,
                        Value = "<div><p>Click on login</p></div>"
                    },
                    new stepsStepParameterizedString()
                    {
                        isformatted = true,
                        Value = "<div><p>It should works</p></div>"
                    }
                }
            }
        };

        var testCase = await Connection.CreateWorkItem("Test case", projectId, testCaseTitle, new Dictionary<string, string>()
        {
            { "Microsoft.VSTS.TCM.Steps", newSteps.ToWorkItemField() }
        });

        await Connection.LinkAsChild(wis[0].WorkItem, testCase);
    }
}
