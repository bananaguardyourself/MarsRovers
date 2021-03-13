using System;
using System.Collections.Generic;
using System.Text;
using Business.MarsRovers.Entities;
using Business.MarsRovers.Interfaces;
using Contracts.Enums;
using Infrastructure.Exceptions.Aerdata.Maintenance.Infrastructure.Exceptions;

namespace Business.MarsRovers.CommandProcessor
{
	public class MoveForwardCommandProcess : ICommandProcessor
	{
		public Position ProcessCommand(Position position)
		{
			return position.Direction switch
			{
				Direction.North => new Position() { X = position.X, Y = position.Y + 1, Direction = position.Direction },
				Direction.East => new Position() { X = position.X + 1, Y = position.Y, Direction = position.Direction },
				Direction.South => new Position() { X = position.X, Y = position.Y - 1, Direction = position.Direction },
				Direction.West => new Position() { X = position.X - 1, Y = position.Y, Direction = position.Direction },
				_ => throw new MarsRoversValidationException("MoveForwardCommand", $"Invalid rover direction {position.Direction}")
			};
		}
	}
}
