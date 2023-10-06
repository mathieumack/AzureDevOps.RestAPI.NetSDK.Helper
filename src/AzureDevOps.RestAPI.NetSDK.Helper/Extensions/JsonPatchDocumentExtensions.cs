using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using System;

namespace AzureDevOps.RestAPI.NetSDK.Helper;

public static class JsonPatchDocumentExtensions
{
    public static void AddJsonPathOperation(this JsonPatchDocument patchDocument,
                                            Microsoft.VisualStudio.Services.WebApi.Patch.Operation operation,
                                            string path,
                                            string value)
    {
        if (patchDocument == null)
            throw new ArgumentNullException(nameof(patchDocument));

        patchDocument.Add(new JsonPatchOperation()
        {
            Operation = operation,
            Path = path,
            Value = value
        });
    }
}
