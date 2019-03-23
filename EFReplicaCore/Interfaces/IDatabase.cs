using EFReplicaCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFReplicaCore.Interfaces
{
    public interface IDatabase
    {
        Repository<Entity> GetRepository(Type T);
        bool CreateDatabase(Type[] types);
    }
}
