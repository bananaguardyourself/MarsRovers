using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.MarsRovers;
using Business.MarsRovers.Interfaces;
using Contracts.MarsRovers.Models;
using FluentValidation;
using MarsRovers.Controllers.MarsRovers.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace MarsRovers.Configuration
{
	public static class DependencyRegistration
	{
		public static void BuildDependancies(IServiceCollection services)
		{
			// Managers
			services.AddScoped<IValidator<MarsRoversModel>, MarsRoversValidator>();

			// Validators
			services.AddScoped<IMarsRoversManager, MarsRoversManager>();
		}
	}
}
