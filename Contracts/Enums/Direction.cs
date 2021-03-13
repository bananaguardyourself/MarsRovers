using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Contracts.Enums
{
	public enum Direction
	{
		[Description("N")]
		North,
		[Description("E")]
		East,
		[Description("S")]
		South,
		[Description("W")]
		West,
	}
}
