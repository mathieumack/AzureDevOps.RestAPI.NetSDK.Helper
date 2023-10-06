using System;
using System.Collections.Generic; 
using System.Threading.Tasks;
using Microsoft.VisualStudio.Services.WebApi;
using Microsoft.TeamFoundation.Core.WebApi;
using AzureDevOps.RestAPI.NetSDK.Helper.AzdoProjects.Domain;
using System.Linq;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;

namespace AzureDevOps.RestAPI.NetSDK.Helper.AzdoProjects.Extensions
{
    public static class ProjectsExtensions
    {
        /// <summary>
        /// Get project with the specified id or name
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="id">Project identifier</param>
        /// <returns></returns>
        public static async Task<ProjectDetail> GetProject(this VssConnection connection, string id)
        {
            var client = connection.GetClient<ProjectHttpClient>();
            var project = await client.GetProject(id);

            // Prepare for new fields like default area path or list of available areas
            //var _witClient = connection.GetClient<WorkItemTrackingHttpClient>();
            //var areaPathNode = await _witClient.GetClassificationNodeAsync(project.Name, TreeStructureGroup.Areas, depth: 1);

            return new ProjectDetail()
            {
                Id = project.Id,
                Name = project.Name
            };
        }


        /// <summary>
        /// Return a list of project detail
        /// Can generate exception if query failed
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static async Task<List<ProjectDetail>> GetProjectList(this VssConnection connection)
        {
            var client = connection.GetClient<ProjectHttpClient>();
            var projects = await client.GetProjects();
            return projects.Select(e => new ProjectDetail()
            {
                Id = e.Id,
                Name = e.Name
            }).ToList();
        }

        /// <summary>
        /// Return a list of project detail
        /// In case of error, return an empty list
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static async Task<List<ProjectDetail>> TryGetProjectList(this VssConnection connection)
        {
            try
            {
                return await connection.GetProjectList();
            }
            catch
            {
                return new List<ProjectDetail>();
            }
        }
    }
}
