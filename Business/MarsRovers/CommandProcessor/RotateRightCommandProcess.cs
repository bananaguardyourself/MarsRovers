using System;
using System.Collections.Generic;
using System.Text;
using Business.MarsRovers.Entities;
using Business.MarsRovers.Interfaces;
using Contracts.Enums;
using Infrastructure.Exceptions.Aerdata.Maintenance.Infrastructure.Exceptions;

namespace Business.MarsRovers.CommandProcessor
{
	public class RotateRightCommandProcess : ICommandProcessor
	{
		public Position ProcessCommand(Position position)
		{
			return position.Direction switch
			{
				Direction.North => new Position() { X = position.X, Y = position.Y, Direction = Direction.East },
				Direction.East => new Position() { X = position.X, Y = position.Y, Direction = Direction.South },
				Direction.South => new Position() { X = position.X, Y = position.Y, Direction = Direction.West },
				Direction.West => new Position() { X = position.X, Y = position.Y, Direction = Direction.North },
				_ => throw new MarsRoversValidationException("RotateRightCommand", $"Invalid rover direction {position.Direction}")
			};
		}
	}
}
