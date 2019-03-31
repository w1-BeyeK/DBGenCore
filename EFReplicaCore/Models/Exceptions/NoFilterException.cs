using System;
using System.Collections.Generic;
using System.Text;

namespace EFReplicaCore.Models.Exceptions
{
    public class NoFilterException: Exception
    {
        public NoFilterException()
        {
        }

        public NoFilterException(string message)
            : base(message)
        {
        }

        public NoFilterException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
