using EFReplicaCore.Interfaces;
using EFReplicaCore.Models.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFReplicaCore.Models
{
    public class MSSQLDatabase : IDatabase
    {
        public List<Repository<Entity>> Repositories { get; set; }

        public bool CreateDatabase(Type[] types)
        {
            try
            {
                foreach (Type type in types)
                {
                    //Repositories.Add(new Repository<Type>(new MSSQLContext<Type>()));
                }
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public Repository<Entity> GetRepository(Type T)
        {
            throw new NotImplementedException();
        }
    }
}
