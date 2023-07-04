using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using System.Collections.Generic;

namespace AzureDevOps.RestAPI.NetSDK.Helper.AzdoWorkItems.Domain
{
    public class WorkItemResult
    {
        /// <summary>
        /// Work item id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Work item element. Populated if requested during query. Null instead
        /// </summary>
        public WorkItem WorkItem { get; set; }

        /// <summary>
        /// Work item element. Populated if requested during query. Null instead
        /// </summary>
        public List<WorkItemComment> WorkItemComments { get; set; }

        /// <summary>
        /// Child items in case of Tree query
        /// </summary>
        public List<WorkItemResult> Children { get; set; }
    }
}
