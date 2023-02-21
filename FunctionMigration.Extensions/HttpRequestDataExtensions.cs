using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Web;

namespace Microsoft.Azure.Functions.Worker.Http;

public static class HttpRequestDataExtensions
{

	public static string Form(this HttpRequestData req, string key)
	{

		var formData = new FormReader(req.Body).ReadFormAsync().GetAwaiter().GetResult();
		// var formData = new FormCollection(JsonSerializer.Deserialize<Dictionary<string, Microsoft.Extensions.Primitives.StringValues>>(requestBody));
		return formData[key];

	}

	public static string Query(this HttpRequestData req, string key)
	{
		var query = HttpUtility.ParseQueryString(req.Url.Query);
		return query[key];
	}

	public static Dictionary<string,string?> Query(this HttpRequestData req)
	{
		var query = HttpUtility.ParseQueryString(req.Url.Query);

		var outDict = new Dictionary<string, string?>();
		foreach (string key in query.Keys)
		{
			outDict.Add(key, query[key]);
		}
		return outDict;

	}

	public static IEnumerable<string> Header(this HttpRequestData req, string key)
	{
		return req.Headers.FirstOrDefault(x => x.Key == key).Value ?? Array.Empty<string>();
	}
}
