using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.MarsRovers.Resources
{
	public class RoverPositionResource
	{
		public int PositionX { get; set; }
		public int PositionY { get; set; }
		public string Direction { get; set; }
		public bool Success { get; set; }
		public string StatusMessage { get; set; }
	}
}
