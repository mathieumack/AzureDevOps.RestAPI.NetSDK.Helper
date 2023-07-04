using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi; 

namespace AzureDevOps.RestAPI.NetSDK.Helper.Connection.Extensions
{
    public static class VssConnectionExtensions
    {
        /// <summary>
        /// Create a VssConnection based on uri and personal access token
        /// </summary>
        /// <param name="collectionUri">Uri of the collection. Ex : https://dev.azure.com/xxx </param>
        /// <param name="personalAccessToken"></param>
        public static VssConnection GetVssConnection(string collectionUri,
                                                     string personalAccessToken)
        {
            var connection = new VssConnection(
                                    new Uri(collectionUri),
                                    new VssBasicCredential(string.Empty, personalAccessToken));

            return connection;
        }

        /// <summary>
        /// Register VssConnection as singleton in the IOC.
        /// </summary>
        /// <param name="serviceCollection">service collection that will be used to register singleton</param>
        /// <param name="collectionUri">Uri of the collection. Ex : https://dev.azure.com/xxx </param>
        /// <param name="personalAccessToken"></param>
        public static void RegisterVssConnection(this IServiceCollection serviceCollection,
                                                    string collectionUri, 
                                                    string personalAccessToken)
        {
            var connection = new VssConnection(
                                    new Uri(collectionUri),
                                    new VssBasicCredential(string.Empty, personalAccessToken));
            
            serviceCollection.AddSingleton(connection);
        }
    }
}