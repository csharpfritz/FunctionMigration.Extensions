using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Test.IsolatedFunctions;

public class Function1
{
	private readonly ILogger _logger;

	public Function1(ILoggerFactory loggerFactory)
	{
		_logger = loggerFactory.CreateLogger<Function1>();
	}

	/// <summary>
	/// Default function that is added to introduce HttpTrigger and working with functions
	/// </summary>
	/// <param name="req">The HttpRequest that triggered this function</param>
	/// <returns>Response data to send back to the browser</returns>
	[Function("Function1")]
	public HttpResponseData HelloWorldFirstFunction([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
	{
		_logger.LogInformation("C# HTTP trigger function processed a request.");

		var response = req.CreateResponse(HttpStatusCode.OK);
		response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

		response.WriteString("Welcome to Azure Functions!");

		return response;
	}
}