using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.MarsRovers.Entities;

namespace Business.MarsRovers.Validators
{
	public static class PositionValidator
	{
		private const string _deployCollisionError = "Rover can't be deployed because the position {0}, {1} occupied by other rover!";
		private const string _deployDimensionError = "Rover can't be deployed because the position {0}, {1} is out of plateau!";
		private const string _collisionError = "Rover can't move further because the position {0}, {1} occupied by other rover!";
		private const string _dimensionError = "Rover can't move further because the position {0}, {1} is out of plateau!";

		public static void ValidatePosition(Rover rover, Position nextPosition, Dictionary<int, Rover> rovers, int sizeX, int sizeY, bool initial = false)
		{
			if (!rover.Processed)
			{
				if (nextPosition.X > sizeX || nextPosition.Y > sizeY || nextPosition.X < 0 || nextPosition.Y < 0)
				{
					rover.Processed = true;
					rover.Success = false;
					rover.Deployed = false;
					rover.StatusMessage = initial
						? string.Format(_deployDimensionError, nextPosition.X, nextPosition.Y)
						: string.Format(_dimensionError, nextPosition.X, nextPosition.Y);
				}

				foreach (var placedRover in initial ? rovers.Where(x => x.Key < rover.Id) : rovers.Where(x => x.Key != rover.Id))
				{
					if (nextPosition.Equals(placedRover.Value.CurrentPosition))
					{
						rover.Processed = true;
						rover.Success = false;
						rover.Deployed = false;
						rover.StatusMessage = initial
							? string.Format(_deployCollisionError, nextPosition.X, nextPosition.Y)
							: string.Format(_collisionError, nextPosition.X, nextPosition.Y);
					}
				}
			}
		}
	}
}
