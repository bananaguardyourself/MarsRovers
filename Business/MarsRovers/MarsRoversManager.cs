using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.MarsRovers.CommandProcessor;
using Business.MarsRovers.Entities;
using Business.MarsRovers.Interfaces;
using Business.MarsRovers.Parsers;
using Business.MarsRovers.Validators;
using Contracts.Enums;
using Contracts.MarsRovers.Models;
using Contracts.MarsRovers.Resources;
using Infrastructure.Helpers;
using Microsoft.Extensions.Logging;

namespace Business.MarsRovers
{
	public class MarsRoversManager : IMarsRoversManager
	{
		private readonly ILogger<MarsRoversManager> _logger;

		public MarsRoversManager(ILogger<MarsRoversManager> logger)
		{
			_logger = logger;
		}

		public MarsRoversResource CalculateRoversPosition(MarsRoversModel model)
		{
			var result = new MarsRoversResource();
			var rovers = new Dictionary<int, Rover>();

			var directions = EnumDescriptionHelper.GetEnumDescriptions<Direction>().ToList();
			var commands = EnumDescriptionHelper.GetEnumDescriptions<RoverCommand>().ToList();

			var index = 0;

			try
			{
				// InitializeRovers
				foreach (var roverModel in model.Rovers)
				{
					index++;
					rovers.Add(index, InitializeRover(index, roverModel, directions, commands));
				};

				// Validate starting positions
				foreach (var rover in rovers)
				{
					PositionValidator.ValidatePosition(rover.Value, rover.Value.CurrentPosition, rovers, model.PlateauX, model.PlateauY, true);
				}

				// ProcessRovers
				var deployedRovers = rovers.Where(x => x.Value.Deployed).ToDictionary(x => x.Key, x => x.Value); ;
				foreach (var rover in deployedRovers)
				{
					ProcessRoverCommands(rover.Value, deployedRovers, model.PlateauX, model.PlateauY);
				}

				// Map results
				result = MapRoversToResource(rovers);

			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				throw;
			}

			return result;
		}

		internal MarsRoversResource MapRoversToResource(Dictionary<int, Rover> rovers)
		{
			var result = new MarsRoversResource();
			foreach (var rover in rovers)
			{
				result.RoverPostitions.Add(new RoverPositionResource()
				{
					Direction = rover.Value.CurrentPosition.Direction.GetEnumDescription(),
					PositionX = rover.Value.CurrentPosition.X,
					PositionY = rover.Value.CurrentPosition.Y,
					StatusMessage = rover.Value.StatusMessage,
					Success = rover.Value.Success
				});
			}

			return result;
		}

		internal void ProcessRoverCommands(Rover rover, Dictionary<int, Rover> rovers, int horizontalSize, int verticalSize)
		{
			var commandFactory = new CommandFactory();

			foreach (var command in rover.Commands)
			{
				if (!rover.Processed)
				{
					var nextPosition = commandFactory.ProcessCommand(command, rover.CurrentPosition);

					if (command == RoverCommand.Move)
					{
						PositionValidator.ValidatePosition(rover, nextPosition, rovers, horizontalSize, verticalSize);
					}

					if (!rover.Processed)
					{
						rover.CurrentPosition = nextPosition;
					}
				}
			}

			rover.Processed = true;
		}

		internal Rover InitializeRover(int index, RoverProgramModel model, List<string> directions, List<string> commands)
		{
			var rover = new Rover()
			{
				Id = index
			};

			var roverPosition = new Position()
			{
				X = model.InitialX,
				Y = model.InitialY,
				Direction = DirectionParser.GetDirection(model.InitialDirection.ToUpper(), directions)
			};

			rover.CurrentPosition = roverPosition;
			rover.Commands = CommandParser.GetCommands(model.Commands.ToUpper(), commands);

			return rover;
		}
	}
}
