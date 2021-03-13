using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRovers.Infrastructure
{
	public static class ValidationErrorCodes
	{
		public static readonly ValidationErrorCode ValidationErrors = new ValidationErrorCode("0", "Validation errors occured.");
		public static ValidationErrorCode FieldIsNotGreaterOrEqual<T>(T value)
		{
			return new ValidationErrorCode("1", $"Field value has to be greater than or equal to {value}");
		}

		public static ValidationErrorCode FieldLenghtIsInvalid<T>(T value)
		{
			return new ValidationErrorCode("2", $"Field length has to be {value}");
		}		
	}

	public class ValidationErrorCode
	{
		public string Code { get; set; }
		public string Message { get; set; }

		public ValidationErrorCode(string code, string message)
		{
			Code = code;
			Message = message;
		}
	}
}
