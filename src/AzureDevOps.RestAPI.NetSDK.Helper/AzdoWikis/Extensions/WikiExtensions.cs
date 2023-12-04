using Microsoft.TeamFoundation.Wiki.WebApi;
using Microsoft.TeamFoundation.Wiki.WebApi.Contracts;
using Microsoft.VisualStudio.Services.WebApi;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AzureDevOps.RestAPI.NetSDK.Helper.AzdoWikis.Extensions;

public static class WikiExtensions
{
    /// <summary>
    /// List all available wikis in the project
    /// </summary>
    /// <param name="connection"></param>
    /// <returns></returns>
    public static async Task<List<WikiV2>> GetWikis(this VssConnection connection)
    {
        var client = connection.GetClient<WikiHttpClient>();

        return await client.GetAllWikisAsync();
    }

    /// <summary>
    /// Extract all pages from the Wiki
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="wikiIdentifier"></param>
    /// <returns></returns>
    public static async Task<WikiV2> GetWiki(this VssConnection connection, string wikiIdentifier)
    {
        var client = connection.GetClient<WikiHttpClient>();

        return await client.GetWikiAsync(wikiIdentifier);
    }

    /// <summary>
    /// Retreive all wiki pages for a specific wiki in a project
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="project">Project guid or project name</param>
    /// <param name="wikiIdentifier"></param>
    /// <returns></returns>
    public static async Task<List<WikiPageDetail>> GetWikiPages(this VssConnection connection, string project, string wikiIdentifier)
    {
        var client = connection.GetClient<WikiHttpClient>();

        var result = new List<WikiPageDetail>();

        var batch = new WikiPagesBatchRequest();

        var pages = await client.GetPagesBatchAsync(batch, project, wikiIdentifier);
        result.AddRange(pages);
        while (!string.IsNullOrWhiteSpace(pages.ContinuationToken))
        {
            batch.ContinuationToken = pages.ContinuationToken;
            pages = await client.GetPagesBatchAsync(batch, project, wikiIdentifier);
            result.AddRange(pages);
        }

        return result;
    }

    /// <summary>
    /// Get wiki page text content
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="project">Project guid or project name</param>
    /// <param name="wikiIdentifier"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    public static async Task<string> GetWikiPageTextById(this VssConnection connection, 
                                                        string project, 
                                                        string wikiIdentifier, 
                                                        int wikiPageId)
    {
        var client = connection.GetClient<WikiHttpClient>();

        var page = await client.GetPageByIdAsync(project, wikiIdentifier, wikiPageId);
        return page.Page.Content;
    }

    ///// <summary>
    ///// Get wiki page text content
    ///// </summary>
    ///// <param name="connection"></param>
    ///// <param name="project">Project guid or project name</param>
    ///// <param name="wikiIdentifier"></param>
    ///// <param name="path"></param>
    ///// <returns></returns>
    //public static async Task<string> GetWikiPageTextByFileName(this VssConnection connection, string project, string wikiIdentifier, string path)
    //{
    //    var client = connection.GetClient<WikiHttpClient>();

    //    using (var stream = await client.get(project, wikiIdentifier, path))
    //    {
    //        using (StreamReader sr = new StreamReader(stream))
    //        {
    //            return sr.ReadToEnd();
    //        }
    //    }
    //}

    /// <summary>
    /// Get wiki page text content
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="project">Project guid or project name</param>
    /// <param name="wikiIdentifier"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    public static async Task<string> GetWikiPageTextByPath(this VssConnection connection, string project, string wikiIdentifier, string path)
    {
        var client = connection.GetClient<WikiHttpClient>();

        using (var stream = await client.GetPageTextAsync(project, wikiIdentifier, path))
        {
            using (StreamReader sr = new StreamReader(stream))
            {
                return sr.ReadToEnd();
            }
        }
    }
}
