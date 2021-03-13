using System;
using System.Collections.Generic;
using System.Text;
using Contracts.MarsRovers.Models;
using MarsRovers.Controllers.MarsRovers.Validators;
using MarsRovers.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MarsRovers.Test
{
	[TestClass]
	public class MarsRoversValidatorTests
	{
		[TestMethod]
		public void Validate_AllCorrectValuesModel_Success()
		{
			var validator = new MarsRoversValidator();
			var model = new MarsRoversModel()
			{
				PlateauX = 3,
				PlateauY = 6,
				Rovers = new List<RoverProgramModel>()
			};

			var validationResult = validator.ValidateModelIfNotNull(model);

			Assert.IsTrue(validationResult.IsValid);
		}

		[TestMethod]
		public void Validate_AllIncorrectValuesModel_Fail()
		{
			var validator = new MarsRoversValidator();
			var model = new MarsRoversModel()
			{
				PlateauX = -2,
				PlateauY = -5,
				Rovers = new List<RoverProgramModel>()
			};

			var validationResult = validator.ValidateModelIfNotNull(model);

			Assert.IsFalse(validationResult.IsValid);
			Assert.AreEqual(2, validationResult.Errors.Count);

			Assert.AreEqual("1", validationResult.Errors[0].ErrorCode);
			Assert.AreEqual("Field value has to be greater than or equal to 0", validationResult.Errors[0].ErrorMessage);
			Assert.AreEqual("PlateauY", validationResult.Errors[0].PropertyName);

			Assert.AreEqual("1", validationResult.Errors[1].ErrorCode);
			Assert.AreEqual("Field value has to be greater than or equal to 0", validationResult.Errors[1].ErrorMessage);
			Assert.AreEqual("PlateauX", validationResult.Errors[1].PropertyName);
		}
	}
}
