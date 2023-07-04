using System;

namespace AzureDevOps.RestAPI.NetSDK.Helper.AzdoWorkItems.Domain
{
    /// <summary>
    /// Detail of a query object
    /// </summary>
    public class QueryDetail
    {
        /// <summary>
        /// Identifier of the query
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Identifier of the project
        /// </summary>
        public Guid ProjectId { get; set; }

        /// <summary>
        /// Name of the project
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Full qualified path with name
        /// </summary>
        public string FullPath { get; set; }
    }
}
