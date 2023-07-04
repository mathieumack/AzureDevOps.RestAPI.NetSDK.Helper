using System;
using System.Collections.Generic;
using System.Text;

namespace AzureDevOps.RestAPI.NetSDK.Helper.AzdoProjects.Domain
{
    /// <summary>
    /// Project detail
    /// </summary>
    public class ProjectDetail
    {
        /// <summary>
        /// Identifier of the project
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Name of the project
        /// </summary>
        public string Name { get; set; }
    }
}
