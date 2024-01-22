using Microsoft.VisualStudio.Services.WebApi;
using System.Collections.Generic;
using AzureDevOps.RestAPI.NetSDK.Helper.AzdoTests.Domain;
using System.Threading.Tasks;
using AzureDevOps.RestAPI.NetSDK.Helper.AzdoWorkItems.Extensions;
using AzureDevOps.RestAPI.NetSDK.Helper.AzdoWorkItems.Domain;
using System.IO;
using System.Xml.Serialization;
using System.Text;
using System.Xml;

namespace AzureDevOps.RestAPI.NetSDK.Helper.AzdoTests.Extensions;

public static class TestCasesExtensions
{
    /// <summary>
    /// Return all work item test cases
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public static async Task<List<WorkItemResult>> GetWorkItemTestCases(this VssConnection connection,
                                                                    int id)
    {
        return await connection.GetQueryResultsByWiql($@"
                    select [System.Id]
                    from WorkItems
                    where  [System.WorkItemType] = 'Test Case' and [System.Parent] = {id}", true, false, false, false);
    }

    public static steps GetTestSteps(this WorkItemResult workItem)
    {
        var stepsContent = workItem.WorkItem.Fields["Microsoft.VSTS.TCM.Steps"].ToString();

        using (TextReader stepsReader = new StringReader(stepsContent))
        {
            var serializer = new XmlSerializer(typeof(steps));
            return (steps)serializer.Deserialize(stepsReader);
        }
    }

    public static string ToWorkItemField(this steps steps)
    {
        StringBuilder result = new StringBuilder();
        var serializer = new XmlSerializer(typeof(steps));
        using (var writer = XmlWriter.Create(result))
        {
            serializer.Serialize(writer, steps);
        }

        return result.ToString();
    }
}