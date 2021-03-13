using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Contracts.Enums
{
	public enum RoverCommand
	{
		[Description("M")]
		Move,
		[Description("L")]
		Left,
		[Description("R")]
		Right,
	}
}
