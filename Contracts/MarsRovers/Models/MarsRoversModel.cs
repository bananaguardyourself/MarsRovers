using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Contracts.MarsRovers.Models
{
	public class MarsRoversModel
	{
		[Required]
		public int PlateauX { get; set; }
		[Required]
		public int PlateauY { get; set; }
		public List<RoverProgramModel> Rovers { get; set; } = new List<RoverProgramModel>();
	}
}
