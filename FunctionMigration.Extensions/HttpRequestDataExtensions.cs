using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Azure.Functions.Worker.Http;
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

	public static IEnumerable<string> Header(this HttpRequestData req, string key)
	{
		return req.Headers.FirstOrDefault(x => x.Key == key).Value ?? Array.Empty<string>();
	}
}
