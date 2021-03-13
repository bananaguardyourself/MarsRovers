using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Infrastructure.Exceptions
{
    namespace Aerdata.Maintenance.Infrastructure.Exceptions
    {
        [Serializable]
        public class MarsRoversValidationException : MarsRoversBaseException
        {
            public override MarsRoversExceptionCodes Code => MarsRoversExceptionCodes.Validation;
            public MarsRoversValidationException(string target, string message)
                : base(target, message)
            {
            }

            protected MarsRoversValidationException(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }
        }
    }
}
