using EFReplicaCore.Interfaces;
using EFReplicaCore.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EFReplicaCore.Models
{
    public class Repository<T> where T : Entity
    {
        private readonly IContext<T> context;

        public Repository(IContext<T> context)
        {
            this.context = context;
        }

        public void Insert(T obj)
        {
            this.context.Insert(obj);
        }

        public void Update(T obj)
        {
            this.context.Update(obj);
        }

        public void Delete(T obj)
        {
            this.context.Delete(obj);
        }

        public void Delete(long id)
        {
            Delete(GetWithFilter(
                new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("id", id)
                }).FirstOrDefault());
        }

        public List<T> GetWithFilter(List<KeyValuePair<string, object>> filters)
        {
            return this.context.GetWithFilter(filters);
        }

        public T GetById(long id)
        {
            return GetWithFilter(
                new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("Id", id)
                }).FirstOrDefault();
        }
    }
}
