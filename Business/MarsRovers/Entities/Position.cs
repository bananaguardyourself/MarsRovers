using System;
using System.Collections.Generic;
using System.Text;
using Contracts.Enums;

namespace Business.MarsRovers.Entities
{
	public class Position
	{
		public int X { get; set; }
		public int Y { get; set; }
		public Direction Direction { get; set; }

		public override bool Equals(Object obj)
		{
			if ((obj == null) || !this.GetType().Equals(obj.GetType()))
			{
				return false;
			}
			else
			{
				Position p = (Position)obj;
				return (X == p.X) && (Y == p.Y);
			}
		}

		public override int GetHashCode()
		{
			return (X << 2) ^ Y;
		}
	}
}
