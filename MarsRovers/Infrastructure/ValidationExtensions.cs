using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace MarsRovers.Infrastructure
{
	public static class ValidationExtensions
	{
		public static ValidationResult ValidateModelIfNotNull<T>(this IValidator<T> validator, T model)
		{
			return model == null
				? new ValidationResult(new[] { new ValidationFailure(typeof(T).Name, "Model cannot be null") })
				: validator.Validate(model);
		}

		public static IRuleBuilder<T, int> FieldGreaterThanOrEqualTo<T>(this IRuleBuilder<T, int> ruleBuilder, int value)
		{
			var error = ValidationErrorCodes.FieldIsNotGreaterOrEqual(value);

			return ruleBuilder
				.Must(p => p >= value)
				.WithErrorCode(error.Code)
				.WithMessage(error.Message);
		}

		public static IRuleBuilderOptions<T, string> FieldLength<T>(this IRuleBuilder<T, string> ruleBuilder, int value)
		{
			var error = ValidationErrorCodes.FieldLenghtIsInvalid(value);

			return ruleBuilder
				.Length(value)
				.WithErrorCode(error.Code)
				.WithMessage(error.Message);
		}
	}
}
