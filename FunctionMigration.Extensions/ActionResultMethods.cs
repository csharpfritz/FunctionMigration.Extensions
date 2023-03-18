using System.Net;

namespace Microsoft.Azure.Functions.Worker.Http;

public static class ActionResultMethods
{

	public static HttpResponseData AcceptedResult(this HttpRequestData req)
	{

		var res = req.CreateResponse();
		res.StatusCode = HttpStatusCode.Accepted;
		return res;

	}

	public static HttpResponseData AcceptedResult(this FunctionContext context)
	{

		var res = context.GetHttpResponseData();
		res.StatusCode = HttpStatusCode.Accepted;
		return res;

	}

	public static HttpResponseData BadRequestObjectResult(this HttpRequestData req, object value)
	{
		var res = req.CreateResponse();
		res.StatusCode = HttpStatusCode.BadRequest;
		res.WriteAsJsonAsync(value);
		return res;
	}

	public static HttpResponseData BadRequestObjectResult(this FunctionContext context, object value)
	{

		var res = context.GetHttpResponseData();
		res.StatusCode = HttpStatusCode.BadRequest;
		res.WriteAsJsonAsync(value);
		return res;
	}

	public static HttpResponseData NotFoundResult(this HttpRequestData req)
	{
		var res = req.CreateResponse();
		res.StatusCode = HttpStatusCode.NotFound;
		return res;
	}

	public static HttpResponseData NotFoundResult(this FunctionContext context)
	{
		var res = context.GetHttpResponseData();
		res.StatusCode = HttpStatusCode.NotFound;
		return res;
	}

	public static HttpResponseData Ok(this HttpRequestData req)
	{
		var res = req.CreateResponse();
		res.StatusCode = HttpStatusCode.OK;
		return res;
	}

	public static HttpResponseData Ok(this FunctionContext context)
	{
		var res = context.GetHttpResponseData();
		res.StatusCode = HttpStatusCode.OK;
		return res;
	}

	public static HttpResponseData OkResult(this HttpRequestData req) => req.Ok();

	public static HttpResponseData OkResult(this FunctionContext context) => context.Ok();


	public static HttpResponseData OkObjectResult(this HttpRequestData req, object value)
	{
		var res = req.CreateResponse();

		if (value is not null && value.GetType().IsValueType) {
			res.WriteString(value.ToString()!);
		} else if (value is not null && !value.GetType().IsValueType) {
			res.WriteAsJsonAsync(value);
		}
		return res;
	}

	public static HttpResponseData OkObjectResult(this FunctionContext context, object value)
	{

		var res = context.GetHttpResponseData();
		if (value is not null && value.GetType().IsValueType) {
			res.WriteString(value.ToString()!);
		}
		else if (value is not null && !value.GetType().IsValueType) {
			res.WriteAsJsonAsync(value);
		}
		return res;

	}

	public static HttpResponseData UnauthorizedResult(this HttpRequestData req)
	{
		var res = req.CreateResponse();
		res.StatusCode = HttpStatusCode.Unauthorized;
		return res;
	}

	public static HttpResponseData UnauthorizedResult(this FunctionContext context)
	{

		var res = context.GetHttpResponseData();
		if (res == null) return null;

		res.StatusCode = HttpStatusCode.Unauthorized;
		return res;


	}

}