using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contracts.Enums;
using Infrastructure.Exceptions.Aerdata.Maintenance.Infrastructure.Exceptions;
using Infrastructure.Helpers;

namespace Business.MarsRovers.Parsers
{
	public static class CommandParser
	{
		public static RoverCommand GetCommand(string textCommand, List<string> commands)
		{
			if (!commands.Contains(textCommand))
			{
				throw new MarsRoversValidationException("CommandParser", $"Invalid rover command {textCommand}");
			}

			return textCommand.GetValueFromDescription<RoverCommand>();
		}

		public static List<RoverCommand> GetCommands(string textCommands, List<string> commands)
		{
			var commandsList = new List<RoverCommand>();
			foreach (var command in textCommands.ToList())
			{
				commandsList.Add(GetCommand(command.ToString(), commands));
			}

			return commandsList;
		}
	}
}
