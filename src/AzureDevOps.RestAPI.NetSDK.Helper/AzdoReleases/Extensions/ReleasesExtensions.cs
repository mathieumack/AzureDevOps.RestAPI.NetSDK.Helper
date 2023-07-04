using AzureDevOps.RestAPI.NetSDK.Helper.AzdoReleases.Domain;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Contracts;
using Microsoft.VisualStudio.Services.WebApi;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureDevOps.RestAPI.NetSDK.Helper.AzdoReleases.Extensions
{
    public static class ReleasesExtensions
    {
        /// <summary>
        /// Returns informations about current releases
        /// </summary>
        /// <param name="vssConnection"></param>
        /// <param name="loadEnvironments"></param>
        /// <param name="loadManualInterventions"></param>
        /// <param name="loadApprovals"></param>
        /// <returns></returns>
        public static async Task<List<ReleaseResult>> GetReleases(this VssConnection vssConnection, 
                                                                    bool loadEnvironments, 
                                                                    bool loadManualInterventions,
                                                                    bool loadApprovals)
        {
            // ReleaseExpands is a bitmask based on functionnalities you want to load
            var expands = ReleaseExpands.None;
            if (loadEnvironments)
                expands |= ReleaseExpands.Environments;
            if (loadManualInterventions)
                expands |= ReleaseExpands.ManualInterventions;
            if (loadApprovals)
                expands |= ReleaseExpands.Approvals;

            var releaseClient = vssConnection.GetClient<ReleaseHttpClient>();
            var releases = await releaseClient.GetReleasesAsync(queryOrder:ReleaseQueryOrder.Descending, 
                                                                expand:expands);
            return releases.Select(e => new ReleaseResult()
            {
                Id = e.Id,
                Release = e
            }).ToList();
        }
    }
}
