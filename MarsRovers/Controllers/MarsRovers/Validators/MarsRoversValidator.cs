using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.MarsRovers.Models;
using FluentValidation;
using MarsRovers.Infrastructure;

namespace MarsRovers.Controllers.MarsRovers.Validators
{
	public class MarsRoversValidator : AbstractValidator<MarsRoversModel>
	{
		public MarsRoversValidator()
		{			
			RuleFor(p => p.VerticalSize)
				.FieldGreaterThanOrEqualTo(0);

			RuleFor(p => p.HorizontalSize)
				.FieldGreaterThanOrEqualTo(0);

			RuleForEach(p => p.Rovers).SetValidator(new RoverProgramValidator());
		}
	}
}
