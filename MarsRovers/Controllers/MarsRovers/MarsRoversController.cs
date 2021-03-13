using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Business.MarsRovers.Interfaces;
using Contracts.MarsRovers.Models;
using Contracts.MarsRovers.Resources;
using FluentValidation;
using MarsRovers.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace MarsRovers.Controllers.MarsRovers
{
	[ApiController]
	[Route("[controller]")]
	public class MarsRoversController : Controller
	{
		private readonly IValidator<MarsRoversModel> _marsRoversValidator;
		private readonly IMarsRoversManager _marsRoversManager;

		public MarsRoversController(
			IValidator<MarsRoversModel> marsRoversValidator,
			IMarsRoversManager marsRoversManager
		)
		{
			_marsRoversValidator = marsRoversValidator;
			_marsRoversManager = marsRoversManager;

		}

		[HttpPost]
		[ProducesResponseType(typeof(MarsRoversResource), (int)HttpStatusCode.Created)]
		public IActionResult CalculateRoversPosition([FromBody] MarsRoversModel model)
		{
			var validationResult = _marsRoversValidator.ValidateModelIfNotNull(model);

			if (!validationResult.IsValid)
			{
				return ValidationErrorResult.Create(validationResult, nameof(MarsRoversModel));
			}

			var resource = _marsRoversManager.CalculateRoversPosition(model);

			return Created("", resource);
		}
	}
}
