using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.WebApi;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AzureDevOps.RestAPI.NetSDK.Helper.AzdoWorkItems.Extensions
{
    public static class CrudOperationsExtensions
    {
        /// <summary>
        /// Create a work item.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="wiType">Type of work item (Task, Bug, ...)</param>
        /// <param name="projectId">Guid of the project destination</param>
        /// <param name="title">Title of the work item</param>
        /// <param name="properties">List of additional field. Ex : { System.AreaPath, Project1 area }, { System.IterationPath, Sprint 1}</param>
        /// <returns></returns>
        public static async Task<WorkItem> CreateWorkItem(this VssConnection connection,
            string wiType,
            string projectId,
            string title,
            Dictionary<string, string> properties = null)
        {
            // Ok now we can create the work item :
            var newTask = new JsonPatchDocument();

            newTask.AddJsonPathOperation(Microsoft.VisualStudio.Services.WebApi.Patch.Operation.Add,
                                        "/fields/System.TeamProject",
                                        projectId);
            //newTask.AddJsonPathOperation(Microsoft.VisualStudio.Services.WebApi.Patch.Operation.Add,
            //                            "/fields/System.AreaPath",
            //                            contentQuery.resource.revision.fields["System.AreaPath"]);
            //newTask.AddJsonPathOperation(Microsoft.VisualStudio.Services.WebApi.Patch.Operation.Add,
            //                            "/fields/System.IterationPath",
            //                            contentQuery.resource.revision.fields["System.IterationPath"]);
            newTask.AddJsonPathOperation(Microsoft.VisualStudio.Services.WebApi.Patch.Operation.Add,
                                        "/fields/System.Title",
                                        title);
            
            if(properties != null)
            {
                foreach(var  property in properties)
                {
                    newTask.AddJsonPathOperation(Microsoft.VisualStudio.Services.WebApi.Patch.Operation.Add,
                                                $"/fields/{property.Key}",
                                                property.Value);
                }
            }

            var client = connection.GetClient<WorkItemTrackingHttpClient>();
            var result = await client.CreateWorkItemAsync(newTask, projectId, wiType);

            return result;
        }
    }
}
