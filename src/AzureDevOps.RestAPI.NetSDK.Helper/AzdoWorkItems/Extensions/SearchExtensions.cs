﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Search.WebApi;
using Microsoft.VisualStudio.Services.Search.WebApi.Contracts.WorkItem;
using Microsoft.VisualStudio.Services.WebApi;
using WorkItemResult = AzureDevOps.RestAPI.NetSDK.Helper.AzdoWorkItems.Domain.WorkItemResult;

namespace AzureDevOps.RestAPI.NetSDK.Helper.AzdoWorkItems.Extensions
{
    public static class SearchExtensions
    {

        /// <summary>
        /// Loading a specific work item based on the id
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="searchTerm">Search term</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public static async Task<List<WorkItemResult>> Search(this VssConnection connection,
                                                                string searchTerm,
                                                                int top,
                                                                int skip,
                                                                bool includeFacets = false)
        {
            using var client = connection.GetClient<SearchHttpClient>();
            var results = await client.FetchWorkItemSearchResultsAsync(
                new WorkItemSearchRequest()
            {
                SearchText = searchTerm,
                Top = top,
                Skip = skip,
                IncludeFacets = includeFacets
            });

            return await connection.GetQueryResults(results, true, false);
        }


        private static async Task<List<WorkItemResult>> GetQueryResults(this VssConnection connection,
                                                                                WorkItemSearchResponse queryResults,
                                                                                bool loadWorkItemDetails,
                                                                                bool loadWorkItemComments)
        {
            var ids = new List<int>();
            Dictionary<int, WorkItemResult> flatListOfWorkItems = new Dictionary<int, WorkItemResult>();

            foreach (var workitem in queryResults.Results)
            {
                var entry = new WorkItemResult() { 
                    Id = int.Parse(workitem.Fields["system.id"]), 
                    Children = new List<WorkItemResult>() 
                };
                ids.Add(entry.Id);
                flatListOfWorkItems.Add(entry.Id, entry);
            }

            // Now we load the workitem detail :
            var client = connection.GetClient<WorkItemTrackingHttpClient>();

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

            return flatListOfWorkItems.Values.ToList();
        }
    }
}