using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.MarsRovers.Models
{
	public class MarsRoversModel
	{
		public int HorizontalSize { get; set; }
		public int VerticalSize { get; set; }
		public List<RoverProgramModel> Rovers { get; set; }
	}
}
