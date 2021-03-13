using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.MarsRovers.Models
{
	public class RoverProgramModel
	{
		public int InitialX { get; set; }
		public int InitialY { get; set; }
		public string InitialDirection { get; set; }
		public string Commands { get; set; }
	}
}
