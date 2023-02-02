# FunctionMigration.Extensions
A collection of tools, shims, and facades to help make migrating Azure Functions to .NET 7 easier

[![NuGet Package](https://img.shields.io/nuget/v/FunctionMigration.Extensions?color=blue&style=plastic)](https://www.nuget.org/packages/FunctionMigration.Extensions)

## The Problem

You need to migrate your project to .NET 7 and Azure Functions or Azure Static Websites with .NET 7.  The syntax for writing functions with C# and .NET 7 is significantly different due to the isolated model for Azure Functions.

Consider syntax like the following that works properly in Azure Functions in-process mode with .NET 6:

```csharp
[FunctionName("GetClips")]
public async Task<IActionResult> GetClips(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
    ILogger log)
{

    string channelName = req.Form["ChannelName"];

    var top = await _ClipRepository.GetTopClips(channelName, 300);

    return new OkObjectResult(new ClipsPayload
    {
    Data = top,
    TotalClips = top.Count()
    });

}
```

There's so much I like about this method working in Azure Functions... and so much is broken in isolated mode.  This library will help you apply those repairs with minimal changes to your original code.  

With the current 0.1 version, the above function can be adapted to this format and behave the same:

```csharp
[FunctionName("GetClips")]
public async Task<HttpResponseData> GetClips(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequestData req)
{

    string channelName = req.Form("ChannelName");

    var top = await _ClipRepository.GetTopClips(channelName, 300);

    return req.OkObjectResult(new ClipsPayload
    {
        Data = top,
        TotalClips = top.Count()
    });

}
```

## What's in the package?

- A collection of extension methods for HttpRequestData to allow similar ActionResult method types that you may have previously been using
- Extension methods that allow access to Query, Form, and Headers using similar syntax to what you previously used with HttpRequest
- QueueCollector class that will help you replace Queue bindings quickly with ICollector / IAsyncCollector syntax
- Global FunctionName alias that re-routes your existing FunctionName attributes to the new Function attribute.  No re-write needed

## What's planned

- Analyzers and code fixers to help identify changes to be made and help you apply fixes quickly.
- Headers write methods so you can re-use Headers syntax used with HttpRequest
- Docs with step-by-step instructions to migrate to the new project format