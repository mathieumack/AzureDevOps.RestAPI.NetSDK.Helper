using System;
using System.Collections.Generic; 
using System.Threading.Tasks;
using Microsoft.VisualStudio.Services.WebApi;
using Microsoft.TeamFoundation.Core.WebApi;
using AzureDevOps.RestAPI.NetSDK.Helper.AzdoProjects.Domain;
using System.Linq;

namespace AzureDevOps.RestAPI.NetSDK.Helper.AzdoProjects.Extensions
{
    public static class ProjectsExtensions
    {
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
