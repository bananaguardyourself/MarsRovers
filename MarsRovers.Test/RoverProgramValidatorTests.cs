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
	public class RoverProgramValidatorTests
	{
		[TestMethod]
		public void Validate_AllCorrectValuesModel_Success()
		{
			var validator = new RoverProgramValidator();
			var model = new RoverProgramModel()
			{
				InitialX = 1,
				InitialY = 4,
				InitialDirection = "W",
				Commands = "MLRRMmlMM"
			};

			var validationResult = validator.ValidateModelIfNotNull(model);

			Assert.IsTrue(validationResult.IsValid);
		}

		[TestMethod]
		public void Validate_AllIncorrectValuesModel_Fail()
		{
			var validator = new RoverProgramValidator();
			var model = new RoverProgramModel()
			{
				InitialX = -1,
				InitialY = -4,
				InitialDirection = "G",
				Commands = "MLRRmmlMhMD"
			};

			var validationResult = validator.ValidateModelIfNotNull(model);

			Assert.IsFalse(validationResult.IsValid);
			Assert.AreEqual(5, validationResult.Errors.Count);

			Assert.AreEqual("1", validationResult.Errors[0].ErrorCode);
			Assert.AreEqual("Field value has to be greater than or equal to 0", validationResult.Errors[0].ErrorMessage);
			Assert.AreEqual("InitialX", validationResult.Errors[0].PropertyName);

			Assert.AreEqual("1", validationResult.Errors[1].ErrorCode);
			Assert.AreEqual("Field value has to be greater than or equal to 0", validationResult.Errors[1].ErrorMessage);
			Assert.AreEqual("InitialY", validationResult.Errors[1].PropertyName);

			Assert.AreEqual("PredicateValidator", validationResult.Errors[2].ErrorCode);
			Assert.AreEqual("InitialDirection should be one of the following values: [N - North, E - East, S - South, W - West]", validationResult.Errors[2].ErrorMessage);
			Assert.AreEqual("InitialDirection", validationResult.Errors[2].PropertyName);
			Assert.AreEqual("G", validationResult.Errors[2].AttemptedValue);

			Assert.AreEqual("PredicateValidator", validationResult.Errors[3].ErrorCode);
			Assert.AreEqual("Commands should consist of the following values: [M - Move, L - Left, R - Right]", validationResult.Errors[3].ErrorMessage);
			Assert.AreEqual("Commands[8]", validationResult.Errors[3].PropertyName);
			Assert.AreEqual('h', validationResult.Errors[3].AttemptedValue);

			Assert.AreEqual("PredicateValidator", validationResult.Errors[4].ErrorCode);
			Assert.AreEqual("Commands should consist of the following values: [M - Move, L - Left, R - Right]", validationResult.Errors[4].ErrorMessage);
			Assert.AreEqual("Commands[10]", validationResult.Errors[4].PropertyName);
			Assert.AreEqual('D', validationResult.Errors[4].AttemptedValue);
		}
	}
}
