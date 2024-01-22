using AzureDevOps.RestAPI.NetSDK.Helper.AzdoWorkItems.Domain;

namespace AzureDevOps.RestAPI.NetSDK.Helper.AzdoWorkItems.Extensions;

public static class WorkItemResultExtensions
{
    /// <summary>
    /// Get public html link for the work item.
    /// Links should have been retreived from the initial query
    /// </summary>
    /// <param name="workItem"></param>
    /// <returns></returns>
    public static string GetPublicUri(this WorkItemResult workItem)
    {
        return
            (workItem?.WorkItem?.Links?.Links?["html"] as Microsoft.VisualStudio.Services.WebApi.ReferenceLink)?.Href;
    }
}
