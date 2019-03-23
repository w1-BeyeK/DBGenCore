using EFReplicaCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFReplicaCore.Models.Handlers
{
    public class MySQLDatabaseHandler : IHandler
    {
        public bool ExecuteCommand(string query)
        {
            throw new NotImplementedException();
        }

        public object ExecuteSelect(string query)
        {
            throw new NotImplementedException();
        }
    }
}
