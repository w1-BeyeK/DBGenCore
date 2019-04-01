using EFReplicaCore.Interfaces;
using EFReplicaCore.Models.Context;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace EFReplicaCore.Models
{
    public class MSSQLDatabase : IDatabase
    {
        private readonly string connection;
        public List<Repository<Entity>> Repositories = new List<Repository<Entity>>();
        private static Dictionary<string, object> repos = new Dictionary<string, object>();
        
        public MSSQLDatabase(string conn)
        {
            connection = conn;
        }

        private T GetInstanceOf<T>()
        {
            return (T)Activator.CreateInstance(typeof(T), new object[] { });
        }

        public void AddRepository<T>() where T : Entity, new()
        {
            T obj = GetInstanceOf<T>();
            string objName = obj.GetType().Name;
            MSSQLContext<T> context = new MSSQLContext<T>(connection, objName + "s");
            Repository<T> repo = new Repository<T>(context);
            repos.Add(objName + "Repository", repo);
        }

        public object GetRepository(string val)
        {
            return repos.FirstOrDefault(r => r.Key == val).Value;
        }

        public List<Type> GetTypesOfEntity()
        {
            List<Type> objects = new List<Type>();
            foreach (Type type in
                Assembly.GetCallingAssembly().GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(Entity))))
            {
                objects.Add(type);
            }
            return objects;
        }
    }
}
