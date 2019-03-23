using System;
using System.Collections.Generic;
using System.Text;

namespace EFReplicaCore.Interfaces
{
    public interface IHandler
    {
        object ExecuteSelect(string query);
        bool ExecuteCommand(string query);

    }
}
