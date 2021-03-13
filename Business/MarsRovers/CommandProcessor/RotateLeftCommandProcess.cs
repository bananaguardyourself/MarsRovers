using System;
using System.Collections.Generic;
using System.Text;
using Business.MarsRovers.Entities;
using Business.MarsRovers.Interfaces;
using Contracts.Enums;
using Infrastructure.Exceptions.Aerdata.Maintenance.Infrastructure.Exceptions;

namespace Business.MarsRovers.CommandProcessor
{
	public class RotateLeftCommandProcess : ICommandProcessor
	{
		public Position ProcessCommand(Position position)
		{
			return position.Direction switch
			{
				Direction.North => new Position() { X = position.X, Y = position.Y, Direction = Direction.West },
				Direction.East => new Position() { X = position.X, Y = position.Y, Direction = Direction.North },
				Direction.South => new Position() { X = position.X, Y = position.Y, Direction = Direction.East },
				Direction.West => new Position() { X = position.X, Y = position.Y, Direction = Direction.South },
				_ => throw new MarsRoversValidationException("RotateLeftCommand", $"Invalid rover direction {position.Direction}")
			};
		}
	}
}
