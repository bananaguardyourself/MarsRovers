using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Enums;
using Contracts.MarsRovers.Models;
using FluentValidation;
using Infrastructure.Helpers;
using MarsRovers.Infrastructure;

namespace MarsRovers.Controllers.MarsRovers.Validators
{
	public class RoverProgramValidator : AbstractValidator<RoverProgramModel>
	{
		public RoverProgramValidator()
		{
			RuleFor(p => p.InitialX)
				.FieldGreaterThanOrEqualTo(0);

			RuleFor(p => p.InitialY)
				.FieldGreaterThanOrEqualTo(0);

			RuleFor(p => p.InitialDirection)
				.NotNull()
				.NotEmpty()
				.FieldLength(1)
				.Must(p => EnumDescriptionHelper.GetEnumDescriptions<Direction>().Contains(p))
				.WithMessage($"{nameof(RoverProgramModel.InitialDirection)} should be one of the following values: [N - North, E - East, S - South, W - West]");

			RuleForEach(p => p.Commands)								
				.Must(p => EnumDescriptionHelper.GetEnumDescriptions<RoverCommand>().Contains(p.ToString()))
				.When(p => !string.IsNullOrEmpty(p.Commands))
				.WithMessage($"{nameof(RoverProgramModel.Commands)} should consist of the following values: [M - Move, L - Left, R - Right]");
		}
	}
}
