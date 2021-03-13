using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace MarsRovers.Infrastructure
{
	public class ErrorResult : ObjectResult
	{
		public ErrorResult(ValidationErrorResource resource, int statusCode) : base(resource)
		{
			StatusCode = statusCode;
		}
	}
}
