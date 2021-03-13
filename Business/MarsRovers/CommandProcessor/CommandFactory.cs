using System;
using System.Collections.Generic;
using System.Text;
using Business.MarsRovers.Entities;
using Business.MarsRovers.Interfaces;
using Contracts.Enums;
using Infrastructure.Exceptions.Aerdata.Maintenance.Infrastructure.Exceptions;

namespace Business.MarsRovers.CommandProcessor
{
	public class CommandFactory
	{
		public Position ProcessCommand(RoverCommand command, Position position)
		{
			var processor = CreateCommandProcessor(command);
			return processor.ProcessCommand(position);
		}

		private ICommandProcessor CreateCommandProcessor(RoverCommand command)
		{
			return command switch
			{
				RoverCommand.Move => new MoveForwardCommandProcess(),
				RoverCommand.Left => new RotateLeftCommandProcess(),
				RoverCommand.Right => new RotateRightCommandProcess(),
				_ => throw new MarsRoversValidationException("ProcessCommand", $"Invalid rover command {command}")
			};
		}
	}
}
