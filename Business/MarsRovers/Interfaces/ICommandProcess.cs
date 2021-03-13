using System;
using System.Collections.Generic;
using System.Text;
using Business.MarsRovers.Entities;
using Contracts.Enums;

namespace Business.MarsRovers.Interfaces
{
	public interface ICommandProcessor
	{
		Position ProcessCommand(Position position);
	}
}
