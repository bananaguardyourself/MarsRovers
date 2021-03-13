using System;
using System.Collections.Generic;
using System.Text;
using Contracts.Enums;

namespace Business.MarsRovers.Entities
{
	public class Rover
	{
		public int Id { get; set; }
		public Position CurrentPosition { get; set; }
		public List<RoverCommand> Commands { get; set; } = new List<RoverCommand>();
		public bool Success { get; set; } = true;
		public bool Processed { get; set; } = false;
		public bool Deployed { get; set; } = true;
		public string StatusMessage { get; set; } = string.Empty;
	}
}
