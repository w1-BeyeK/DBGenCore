using EFReplicaCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFReplicaCore.Interfaces
{
    public interface IParser
    {
        object Parse<T>(object raw) where T : Entity;
        bool TryParse<T>(object raw, out T result) where T : Entity;
    }
}
