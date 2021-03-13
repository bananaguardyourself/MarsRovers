using System;
using System.Collections.Generic;
using System.Text;
using Contracts.MarsRovers.Models;
using Contracts.MarsRovers.Resources;

namespace Business.MarsRovers.Interfaces
{
	public interface IMarsRoversManager
	{
		MarsRoversResource CalculateRoversPosition(MarsRoversModel model);
	}
}
