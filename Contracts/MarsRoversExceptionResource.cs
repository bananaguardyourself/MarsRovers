using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
	public class MarsRoversExceptionResource : ExceptionResource
	{
		public List<ExceptionResource> Details { get; set; }

		public MarsRoversExceptionResource(string code, string target, string message) : base(code, target, message)
		{
		}
	}
}
