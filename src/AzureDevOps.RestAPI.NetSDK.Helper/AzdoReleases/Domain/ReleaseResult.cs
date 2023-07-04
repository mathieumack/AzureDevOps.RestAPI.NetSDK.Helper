using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureDevOps.RestAPI.NetSDK.Helper.AzdoReleases.Domain
{
    public class ReleaseResult
    {
        public int Id { get; set; }

        public Release Release { get; set; }
    }
}
