using System.Collections.Generic;
using System.Threading.Tasks;
using AzureDevOps.RestAPI.NetSDK.Helper.AzdoWikis.Domain;
using Microsoft.VisualStudio.Services.Search.Shared.WebApi.Contracts.Wiki;
using Microsoft.VisualStudio.Services.Search.WebApi.Contracts.WorkItem;
using Microsoft.VisualStudio.Services.Search.WebApi;
using Microsoft.VisualStudio.Services.WebApi;
using System;

namespace AzureDevOps.RestAPI.NetSDK.Helper.AzdoWikis.Extensions;

public static class SearchExtensions
{
    /// <summary>
    /// Seantic search on product backlog items
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="searchTerm">Search term</param>
    /// <param name="top"></param>
    /// <param name="skip"></param>
    /// <param name="includeFacets"></param>
    /// <returns></returns>
    public static async Task<List<WikiPageResult>> SearchOnWikis(this VssConnection connection,
                                                                        string searchTerm,
                                                                        int top,
                                                                        int skip,
                                                                        bool includeFacets = false)
    {
        using var client = connection.GetClient<SearchHttpClient>();

        var results = new List<WikiPageResult>();

        var workItemsSearch = await client.FetchWikiSearchResultsAsync(
            new WikiSearchRequest()
            {
                SearchText = searchTerm,
                Top = top,
                Skip = skip,
                IncludeFacets = includeFacets
            });
        var workItemsResults = await connection.GetQueryResults(workItemsSearch, true, false);
        results.AddRange(workItemsResults);

        return results;
    }

    private static async Task<List<WikiPageResult>> GetQueryResults(this VssConnection connection,
                                                                            WikiSearchResponse queryResults,
                                                                            bool loadWorkItemDetails,
                                                                            bool loadWorkItemComments)
    {
        var results = new List<WikiPageResult>();

        foreach (var wikiResult in queryResults.Results)
        {
            var entry = new WikiPageResult()
            {
                Id = wikiResult.ContentId,
                WikiId = wikiResult.Wiki.Id,
                Path = wikiResult.Path,
                ProjectId = wikiResult.Project.Id,
                PageContent = ""
            };

            try
            {
                var pageContent = await connection.GetWikiPageTextById(wikiResult.Project.Name, wikiResult.Wiki.Name, int.Parse(wikiResult.ContentId));
                entry.PageContent = pageContent;

                results.Add(entry);
            }
            catch(Exception ex)
            {

            }
        }

        return results;
    }
}
