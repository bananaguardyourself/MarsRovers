using System.Collections.Generic;
using Business.MarsRovers;
using Contracts.MarsRovers.Models;
using Infrastructure.Exceptions;
using Infrastructure.Exceptions.Aerdata.Maintenance.Infrastructure.Exceptions;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MarsRovers.Test
{
	[TestClass]
	public class MarsRoversManagersTests
	{
		private readonly Mock<ILogger<MarsRoversManager>> _logger = new Mock<ILogger<MarsRoversManager>>();

		[TestMethod]
		public void CalculateRoversPosition_TwoValidRoversData_Success()
		{
			// Arrange
			var marsRoverManager = new MarsRoversManager(_logger.Object);

			MarsRoversModel model = new MarsRoversModel()
			{
				PlateauX = 5,
				PlateauY = 5,
				Rovers = new List<RoverProgramModel>()
				{
					new RoverProgramModel()
					{
						InitialX = 1,
						InitialY = 2,
						InitialDirection = "N",
						Commands = "LMLMLMLMM"
					},
					new RoverProgramModel()
					{
						InitialX = 3,
						InitialY = 3,
						InitialDirection = "E",
						Commands = "MMRMMRMRRM"
					},
				}
			};

			// Act
			var result = marsRoverManager.CalculateRoversPosition(model);

			// Assert
			Assert.AreEqual(2, result.RoverPositions.Count);

			Assert.IsTrue(result.RoverPositions[0].Success);
			Assert.IsTrue(string.IsNullOrEmpty(result.RoverPositions[0].StatusMessage));
			Assert.AreEqual(1, result.RoverPositions[0].PositionX);
			Assert.AreEqual(3, result.RoverPositions[0].PositionY);
			Assert.AreEqual("N", result.RoverPositions[0].Direction);

			Assert.IsTrue(result.RoverPositions[1].Success);
			Assert.IsTrue(string.IsNullOrEmpty(result.RoverPositions[1].StatusMessage));
			Assert.AreEqual(5, result.RoverPositions[1].PositionX);
			Assert.AreEqual(1, result.RoverPositions[1].PositionY);
			Assert.AreEqual("E", result.RoverPositions[1].Direction);
		}

		[TestMethod]
		public void CalculateRoversPosition_MissPlateuAndSamePlaceAndTwoValidRovers_TwoSuccesseses()
		{
			// Arrange
			var marsRoverManager = new MarsRoversManager(_logger.Object);

			MarsRoversModel model = new MarsRoversModel()
			{
				PlateauX = 3,
				PlateauY = 3,
				Rovers = new List<RoverProgramModel>()
				{
					new RoverProgramModel()
					{
						InitialX = 4,
						InitialY = 2,
						InitialDirection = "N",
						Commands = "LMLMLMLMM"
					},
					new RoverProgramModel()
					{
						InitialX = 2,
						InitialY = 2,
						InitialDirection = "S",
						Commands = ""
					},
					new RoverProgramModel()
					{
						InitialX = 2,
						InitialY = 2,
						InitialDirection = "E",
						Commands = "MMRMMRMRRM"
					},
					new RoverProgramModel()
					{
						InitialX = 1,
						InitialY = 1,
						InitialDirection = "W",
						Commands = "MRMMRMMM"
					},
				}
			};

			// Act
			var result = marsRoverManager.CalculateRoversPosition(model);

			// Assert
			Assert.AreEqual(4, result.RoverPositions.Count);

			Assert.IsFalse(result.RoverPositions[0].Success);
			Assert.AreEqual("Rover can't be deployed because the position 4, 2 is out of plateau!", result.RoverPositions[0].StatusMessage);
			Assert.AreEqual(4, result.RoverPositions[0].PositionX);
			Assert.AreEqual(2, result.RoverPositions[0].PositionY);
			Assert.AreEqual("N", result.RoverPositions[0].Direction);

			Assert.IsTrue(result.RoverPositions[1].Success);
			Assert.IsTrue(string.IsNullOrEmpty(result.RoverPositions[1].StatusMessage));
			Assert.AreEqual(2, result.RoverPositions[1].PositionX);
			Assert.AreEqual(2, result.RoverPositions[1].PositionY);
			Assert.AreEqual("S", result.RoverPositions[1].Direction);

			Assert.IsFalse(result.RoverPositions[2].Success);
			Assert.AreEqual("Rover can't be deployed because the position 2, 2 occupied by other rover!", result.RoverPositions[2].StatusMessage);
			Assert.AreEqual(2, result.RoverPositions[2].PositionX);
			Assert.AreEqual(2, result.RoverPositions[2].PositionY);
			Assert.AreEqual("E", result.RoverPositions[2].Direction);

			Assert.IsTrue(result.RoverPositions[3].Success);
			Assert.IsTrue(string.IsNullOrEmpty(result.RoverPositions[3].StatusMessage));
			Assert.AreEqual(3, result.RoverPositions[3].PositionX);
			Assert.AreEqual(3, result.RoverPositions[3].PositionY);
			Assert.AreEqual("E", result.RoverPositions[3].Direction);
		}

		[TestMethod]
		public void CalculateRoversPosition_MoveTooFarAndCollision_Unsuccessfull()
		{
			// Arrange
			var marsRoverManager = new MarsRoversManager(_logger.Object);

			MarsRoversModel model = new MarsRoversModel()
			{
				PlateauX = 10,
				PlateauY = 10,
				Rovers = new List<RoverProgramModel>()
				{
					new RoverProgramModel()
					{
						InitialX = 5,
						InitialY = 5,
						InitialDirection = "N",
						Commands = "MMMMMM"
					},
					new RoverProgramModel()
					{
						InitialX = 10,
						InitialY = 10,
						InitialDirection = "W",
						Commands = "MMMMMMLMM"
					},
				}
			};

			// Act
			var result = marsRoverManager.CalculateRoversPosition(model);

			// Assert
			Assert.AreEqual(2, result.RoverPositions.Count);

			Assert.IsFalse(result.RoverPositions[0].Success);
			Assert.AreEqual("Rover can't move further because the position 5, 11 is out of plateau!", result.RoverPositions[0].StatusMessage);
			Assert.AreEqual(5, result.RoverPositions[0].PositionX);
			Assert.AreEqual(10, result.RoverPositions[0].PositionY);
			Assert.AreEqual("N", result.RoverPositions[0].Direction);

			Assert.IsFalse(result.RoverPositions[1].Success);
			Assert.AreEqual("Rover can't move further because the position 5, 10 occupied by other rover!", result.RoverPositions[1].StatusMessage);
			Assert.AreEqual(6, result.RoverPositions[1].PositionX);
			Assert.AreEqual(10, result.RoverPositions[1].PositionY);
			Assert.AreEqual("W", result.RoverPositions[1].Direction);
		}

		[TestMethod]
		public void CalculateRoversPosition_CollisionLastAndCollisionFirstAndValid_OneSuccess()
		{
			// Arrange
			var marsRoverManager = new MarsRoversManager(_logger.Object);

			MarsRoversModel model = new MarsRoversModel()
			{
				PlateauX = 5,
				PlateauY = 5,
				Rovers = new List<RoverProgramModel>()
				{
					new RoverProgramModel()
					{
						InitialX = 2,
						InitialY = 2,
						InitialDirection = "N",
						Commands = "MMLMRM"
					},
					new RoverProgramModel()
					{
						InitialX = 0,
						InitialY = 3,
						InitialDirection = "S",
						Commands = "LMMLMRM"
					},
					new RoverProgramModel()
					{
						InitialX = 2,
						InitialY = 4,
						InitialDirection = "E",
						Commands = "MMMLMLM"
					},
				}
			};

			// Act
			var result = marsRoverManager.CalculateRoversPosition(model);

			// Assert
			Assert.AreEqual(3, result.RoverPositions.Count);

			Assert.IsFalse(result.RoverPositions[0].Success);
			Assert.AreEqual("Rover can't move further because the position 2, 4 occupied by other rover!", result.RoverPositions[0].StatusMessage);
			Assert.AreEqual(2, result.RoverPositions[0].PositionX);
			Assert.AreEqual(3, result.RoverPositions[0].PositionY);
			Assert.AreEqual("N", result.RoverPositions[0].Direction);

			Assert.IsFalse(result.RoverPositions[1].Success);
			Assert.AreEqual("Rover can't move further because the position 2, 3 occupied by other rover!", result.RoverPositions[1].StatusMessage);
			Assert.AreEqual(1, result.RoverPositions[1].PositionX);
			Assert.AreEqual(3, result.RoverPositions[1].PositionY);
			Assert.AreEqual("E", result.RoverPositions[1].Direction);

			Assert.IsTrue(result.RoverPositions[2].Success);
			Assert.IsTrue(string.IsNullOrEmpty(result.RoverPositions[2].StatusMessage));
			Assert.AreEqual(4, result.RoverPositions[2].PositionX);
			Assert.AreEqual(5, result.RoverPositions[2].PositionY);
			Assert.AreEqual("W", result.RoverPositions[2].Direction);
		}

		[TestMethod]
		public void CalculateRoversPosition_MinimalValues_Success()
		{
			// Arrange
			var marsRoverManager = new MarsRoversManager(_logger.Object);

			MarsRoversModel model = new MarsRoversModel()
			{
				PlateauX = 0,
				PlateauY = 0,
				Rovers = new List<RoverProgramModel>()
				{
					new RoverProgramModel()
					{
						InitialX = 0,
						InitialY = 0,
						InitialDirection = "S",
						Commands = "LLLRLRLLLRRL"
					},
				}
			};

			// Act
			var result = marsRoverManager.CalculateRoversPosition(model);

			// Assert
			Assert.AreEqual(1, result.RoverPositions.Count);

			Assert.IsTrue(result.RoverPositions[0].Success);
			Assert.IsTrue(string.IsNullOrEmpty(result.RoverPositions[0].StatusMessage));
			Assert.AreEqual(0, result.RoverPositions[0].PositionX);
			Assert.AreEqual(0, result.RoverPositions[0].PositionY);
			Assert.AreEqual("S", result.RoverPositions[0].Direction);
		}

		[TestMethod]
		public void CalculateRoversPosition_IncorrectDirection_ExceptionThrown()
		{
			// Arrange
			var marsRoverManager = new MarsRoversManager(_logger.Object);

			MarsRoversModel model = new MarsRoversModel()
			{
				PlateauX = 5,
				PlateauY = 5,
				Rovers = new List<RoverProgramModel>()
				{
					new RoverProgramModel()
					{
						InitialX = 2,
						InitialY = 4,
						InitialDirection = "F",
						Commands = "MLLMRRM"
					},
				}
			};

			// Act => Assert
			var ex = Assert.ThrowsException<MarsRoversValidationException>(() => marsRoverManager.CalculateRoversPosition(model));

			Assert.AreEqual("Invalid rover direction F", ex.Message);
			Assert.AreEqual(MarsRoversExceptionCodes.Validation, ex.Code);
			Assert.AreEqual("DirectionParser", ex.Target);
		}

		[TestMethod]
		public void CalculateRoversPosition_IncorrectCommand_ExceptionThrown()
		{
			// Arrange
			var marsRoverManager = new MarsRoversManager(_logger.Object);

			MarsRoversModel model = new MarsRoversModel()
			{
				PlateauX = 5,
				PlateauY = 5,
				Rovers = new List<RoverProgramModel>()
				{
					new RoverProgramModel()
					{
						InitialX = 2,
						InitialY = 4,
						InitialDirection = "W",
						Commands = "KRF"
					},
				}
			};

			// Act => Assert
			var ex = Assert.ThrowsException<MarsRoversValidationException>(() => marsRoverManager.CalculateRoversPosition(model));

			Assert.AreEqual("Invalid rover command K", ex.Message);
			Assert.AreEqual(MarsRoversExceptionCodes.Validation, ex.Code);
			Assert.AreEqual("CommandParser", ex.Target);
		}

		[TestMethod]
		public void CalculateRoversPosition_LowerCaseValues_Success()
		{
			// Arrange
			var marsRoverManager = new MarsRoversManager(_logger.Object);

			MarsRoversModel model = new MarsRoversModel()
			{
				PlateauX = 5,
				PlateauY = 5,
				Rovers = new List<RoverProgramModel>()
				{
					new RoverProgramModel()
					{
						InitialX = 2,
						InitialY = 2,
						InitialDirection = "s",
						Commands = "mmlmmrrm"
					},
				}
			};

			// Act
			var result = marsRoverManager.CalculateRoversPosition(model);

			// Assert
			Assert.AreEqual(1, result.RoverPositions.Count);

			Assert.IsTrue(result.RoverPositions[0].Success);
			Assert.IsTrue(string.IsNullOrEmpty(result.RoverPositions[0].StatusMessage));
			Assert.AreEqual(3, result.RoverPositions[0].PositionX);
			Assert.AreEqual(0, result.RoverPositions[0].PositionY);
			Assert.AreEqual("W", result.RoverPositions[0].Direction);
		}
	}
}
