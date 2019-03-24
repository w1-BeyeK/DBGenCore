using System;
using System.Collections.Generic;
using System.Text;

namespace EFReplicaCore.Models.Exceptions
{
    public class InvalidModelException : Exception
    {
        public InvalidModelException()
        {
        }

        public InvalidModelException(string message)
            : base(message)
        {
        }

        public InvalidModelException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
