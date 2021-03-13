using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;

namespace MarsRovers.Infrastructure
{
	public class ValidationErrorResult : ErrorResult
	{
		public static ValidationErrorResult Create(ValidationResult validationResult, string target)
		{
			return new ValidationErrorResult(new ValidationErrorResource(validationResult, target), StatusCodes.Status422UnprocessableEntity);
		}

		private ValidationErrorResult(ValidationErrorResource resource, int statusCode) : base(resource, statusCode)
		{
		}
	}

	public class ValidationErrorResource : MarsRoversExceptionResource
	{
		public ValidationErrorResource(ValidationResult validationResult, string target)
			: base(ValidationErrorCodes.ValidationErrors.Code, target, ValidationErrorCodes.ValidationErrors.Message)
		{
			Details = validationResult.Errors.Select(e => new ExceptionResource(e.ErrorCode, e.PropertyName, e.ErrorMessage)).ToList();
		}
	}
}
