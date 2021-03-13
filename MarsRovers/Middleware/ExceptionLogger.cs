using Contracts;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Aerdata.Maintenance.Api.Middleware
{
	public class ExceptionLogger
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionLogger> _logger;
		private readonly IHostEnvironment _env;
		public ExceptionLogger(RequestDelegate next, ILogger<ExceptionLogger> logger, IHostEnvironment env)
		{
			_next = next;
			_logger = logger;
			_env = env;
		}
		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (MarsRoversBaseException ex)
			{
				await HandleMarsRoverExceptionAsync(context, ex);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex, StatusCodes.Status500InternalServerError, ex.Message);
			}
		}
		private Task HandleMarsRoverExceptionAsync(HttpContext context, MarsRoversBaseException ex)
		{
			_logger.LogError(ex, string.Empty);
			context.Response.StatusCode = ex.Code switch
			{
				MarsRoversExceptionCodes.ItemNotFound => StatusCodes.Status404NotFound,
				MarsRoversExceptionCodes.Validation => StatusCodes.Status422UnprocessableEntity,
				MarsRoversExceptionCodes.InvalidOperation => StatusCodes.Status403Forbidden,
				_ => StatusCodes.Status422UnprocessableEntity,
			};

			var result = new MarsRoversExceptionResource(ex.Code.ToString(), ex.Target, ex.Message)
			{
				Details = ex.Details.Select(e => new ExceptionResource(e.Code.ToString(), e.Target, e.Message)).ToList()
			};

			if (_env.IsDevelopment())
			{
				result.Exception = ex.ToString();
			}
			context.Response.ContentType = "application/json";
			var json = JsonConvert.SerializeObject(result,
				new JsonSerializerSettings
				{
					ContractResolver = new CamelCasePropertyNamesContractResolver()
				});
			return context.Response.WriteAsync(json);
		}

		private Task HandleExceptionAsync(HttpContext context, Exception ex, int statusCode, string message)
		{
			_logger.LogError(ex, string.Empty);
			context.Response.StatusCode = statusCode;
			var result = new ExceptionResource(statusCode.ToString(),ex.Source,message);
			if (_env.IsDevelopment())
			{
				result.Exception = ex.ToString();
			}
			context.Response.ContentType = "application/json";
			var json = JsonConvert.SerializeObject(result, new JsonSerializerSettings
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver()
			});
			return context.Response.WriteAsync(json);
		}
	}
}