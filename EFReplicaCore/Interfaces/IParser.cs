using System;
using System.Collections.Generic;
using System.Text;

namespace EFReplicaCore.Interfaces
{
    public interface IParser<T>
    {
        T Parse(object raw);
        bool TryParse(object raw, out T result);
    }
}
