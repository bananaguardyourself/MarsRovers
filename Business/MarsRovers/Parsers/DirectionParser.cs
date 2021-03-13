using System;
using System.Collections.Generic;
using System.Text;
using Contracts.Enums;
using Infrastructure.Exceptions.Aerdata.Maintenance.Infrastructure.Exceptions;
using Infrastructure.Helpers;

namespace Business.MarsRovers.Parsers
{
	public static class DirectionParser
	{
		public static Direction GetDirection(string textDirection, List<string> directions)
		{
			if (!directions.Contains(textDirection.ToUpper()))
			{
				throw new MarsRoversValidationException("DirectionParser", $"Invalid rover direction {textDirection}");
			}

			return textDirection.GetValueFromDescription<Direction>();
		}		
	}
}
