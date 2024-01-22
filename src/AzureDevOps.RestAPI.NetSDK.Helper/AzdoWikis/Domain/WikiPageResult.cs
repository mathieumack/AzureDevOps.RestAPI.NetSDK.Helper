using System;

namespace AzureDevOps.RestAPI.NetSDK.Helper.AzdoWikis.Domain;

public class WikiPageResult
{
    public string Id { get; set; }

    public string WikiId { get; set; }

    public Guid ProjectId { get; set; }

    public string PageContent { get; set; }

    public string Path { get; set; }
}
