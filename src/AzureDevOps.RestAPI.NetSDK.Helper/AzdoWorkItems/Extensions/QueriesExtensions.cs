using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureDevOps.RestAPI.NetSDK.Helper.AzdoWorkItems.Domain;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.WebApi;

namespace AzureDevOps.RestAPI.NetSDK.Helper.AzdoWorkItems.Extensions
{
    public static class QueriesExtensions
    {

        /// <summary>
        /// Loading a specific work item based on the id
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="id"></param>
        /// <param name="loadChildren">Not supported yet !</param>
        /// <param name="loadComments">Not supported yet !</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public static async Task<WorkItemResult> GetWorkItem(this VssConnection connection,
                                                                    int id,
                                                                    bool loadChildren,
                                                                    bool loadComments)
        {
            using var client = connection.GetClient<WorkItemTrackingHttpClient>();
            var downloadedWI = await client.GetWorkItemAsync(id, expand: WorkItemExpand.All);

            var result = new WorkItemResult()
            {
                Id = id,
                WorkItem = downloadedWI
            };

            if(loadChildren)
            {
                throw new NotSupportedException("Loading children is not support yet");
            }

            if(loadComments)
            {
                throw new NotSupportedException("Loading comments is not support yet");
            }

            return result;
        }

        /// <summary>
        /// Returns all queries available in a project
        /// Can generate exception if call failed
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static async Task<List<QueryDetail>> GetQueries(this VssConnection connection, Guid projectId)
        {
            using var client = connection.GetClient<WorkItemTrackingHttpClient>();
            var projects = await client.GetQueriesAsync(projectId, QueryExpand.None, 2);
            return projects.Where(e => e.Children != null)
                            .SelectMany(e => e.Children)
                            .Select(e => new QueryDetail()
                                {
                                    Id = e.Id,
                                    ProjectId = projectId,
                                    Name = e.Name,
                                    FullPath = (string.IsNullOrWhiteSpace(e.Path) ? "" : e.Path) + "/" + e.Name
                                })
            .OrderBy(e => e.FullPath).ToList();
        }

        /// <summary>
        /// Returns all queries available in a project
        /// In case of error, return an empty list
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static async Task<List<QueryDetail>> TryGetQueries(this VssConnection connection, Guid projectId)
        {
            try
            {
                return await connection.GetQueries(projectId);
            }
            catch
            {
                return new List<QueryDetail>();
            }
        }

        /// <summary>
        /// Execute a predefined query on Azure DevOps and retreive all results
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="queryId"></param>
        /// <param name="loadWorkItemDetails">True indicate that the result will populate the WorkItem field. Can impact performances</param>
        /// <param name="loadWorkItemComments">True indicate that the result will populate the WorkItemComments field. Can impact performances</param>
        /// <returns></returns>
        public static async Task<List<WorkItemResult>> GetQueryResults(this VssConnection connection,
                                                                        Guid queryId,
                                                                        bool loadWorkItemDetails,
                                                                        bool loadWorkItemComments)
        {
            using var client = connection.GetClient<WorkItemTrackingHttpClient>();
            var queryResults = await client.QueryByIdAsync(queryId);
            return await client.GetQueryResults(queryResults, loadWorkItemDetails, loadWorkItemComments);
        }

        /// <summary>
        /// Search on the work items based on a Wiql query.
        /// More infomrations about Wiql syntax : https://learn.microsoft.com/en-us/azure/devops/boards/queries/wiql-syntax?view=azure-devops
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="wiql"></param>
        /// <param name="loadWorkItemDetails"></param>
        /// <param name="loadWorkItemComments"></param>
        /// <param name="loadRelations"></param>
        /// <returns></returns>
        public static async Task<List<WorkItemResult>> GetQueryResultsByWiql(this VssConnection connection,
                                                                                string wiql,
                                                                                bool loadWorkItemDetails,
                                                                                bool loadWorkItemComments,
                                                                                bool loadRelations,
                                                                                bool loadLinks)
        {
            using var client = connection.GetClient<WorkItemTrackingHttpClient>();
            var queryResults = await client.QueryByWiqlAsync(new Wiql { Query = wiql });

            // Important : (note from documentation)
            // The WIQL syntax is used to execute the Query By Wiql REST API.
            // Currently, there is no way to call the API to return the detailed work item information from a WIQL query directly.
            // No matter which fields you include in the SELECT statement, the API only returns the work item IDs.
            // To get the full information, you need to perform two steps: (1) get the ID of the work items from a WIQL, and (2) get the work items via Get a list of work items by ID and for specific fields.

            // Split all WorkItems to group of 200 as SDK return maximum 200 WorkItems per call
            var groups = queryResults.WorkItems
                .Select((item, index) => new { Value = item.Id, Index = index })
                .GroupBy(e => e.Index / 200, e => e.Value)
                .ToDictionary(e => e.Key, e => e.ToArray());

            var expand = WorkItemExpand.None;
            if (loadRelations)
                expand |= WorkItemExpand.Relations;
            if (loadLinks)
                expand |= WorkItemExpand.Links;

            var result = new List<WorkItemResult>();
            foreach (var group in groups)
            {
                var workItems = await client.GetWorkItemsAsync(group.Value, expand: expand);

                foreach (var workItem in workItems)
                {
                    var wiResult = new WorkItemResult()
                    {
                        Id = workItem.Id.Value,
                        WorkItem = workItem
                    };
                    if (loadWorkItemComments)
                    {
                        var comments = await client.GetCommentsAsync(workItem.Id.Value);
                        wiResult.WorkItemComments = comments.Comments.ToList();
                    }
                    result.Add(wiResult);
                }
            }

            return result;
        }

        private static async Task<List<WorkItemResult>> GetQueryResults(this WorkItemTrackingHttpClient client,
                                                                            WorkItemQueryResult queryResults,
                                                                            bool loadWorkItemDetails,
                                                                            bool loadWorkItemComments)
        { 
            var ids = new List<int>();
            Dictionary<int, WorkItemResult> flatListOfWorkItems = new Dictionary<int, WorkItemResult>();
            List<WorkItemResult> rootItems = new List<WorkItemResult>();

            switch (queryResults.QueryResultType)
            {
                case QueryResultType.WorkItemLink: // Tree
                    foreach (var item in queryResults.WorkItemRelations)
                    {
                        if (!flatListOfWorkItems.ContainsKey(item.Target.Id))
                            flatListOfWorkItems.Add(item.Target.Id, new WorkItemResult() { Id = item.Target.Id, Children = new List<WorkItemResult>() });
                        if (!ids.Contains(item.Target.Id))
                            ids.Add(item.Target.Id);

                        if (item.Source == null) // Root item
                            rootItems.Add(flatListOfWorkItems[item.Target.Id]);
                        else // Source = Parent, Target = Child
                        {
                            flatListOfWorkItems[item.Source.Id].Children.Add(flatListOfWorkItems[item.Target.Id]);
                        }
                    }
                    break;
                default:
                    foreach (var workitem in queryResults.WorkItems)
                    {
                        var entry = new WorkItemResult() { Id = workitem.Id, Children = new List<WorkItemResult>() };
                        rootItems.Add(entry);
                        flatListOfWorkItems.Add(workitem.Id, entry);
                        ids.Add(workitem.Id);
                    }
                    break;
            }

            // Now we load the workitem detail :
            while ((loadWorkItemComments || loadWorkItemDetails) && ids.Count > 0)
            {
                var wiIds = ids.Take(50); // 50 max to limit

                if (loadWorkItemDetails)
                {
                    var downloadedWI = await client.GetWorkItemsAsync(wiIds, expand: WorkItemExpand.All);
                    foreach (var workItem in downloadedWI)
                    {
                        flatListOfWorkItems[workItem.Id.Value].WorkItem = workItem;
                    }
                }

                if (loadWorkItemComments)
                {
                    foreach (var id in wiIds)
                    {
                        flatListOfWorkItems[id].WorkItemComments = new List<WorkItemComment>();
                        var comments = await client.GetCommentsAsync(id);
                        flatListOfWorkItems[id].WorkItemComments.AddRange(comments.Comments);
                    }
                }

                ids.RemoveRange(0, wiIds.Count());
            }

            return rootItems;
        }
    }
}
