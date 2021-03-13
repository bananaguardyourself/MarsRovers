using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Infrastructure.Exceptions
{
	[Serializable]
	public class MarsRoversBaseException : Exception
	{
		public List<MarsRoversBaseException> Details { get; set; }

		public string Error { get; set; }

		public string Target { get; set; }

		public virtual MarsRoversExceptionCodes Code { get; set; }

		public MarsRoversBaseException(string target, string message)
					: base(message)
		{
			Target = target;
			Details = new List<MarsRoversBaseException>();
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("props", Details, typeof(string));
		}

		protected MarsRoversBaseException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
