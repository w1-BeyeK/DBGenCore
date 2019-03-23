using EFReplicaCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EFReplicaCore.Models.Context
{
    public class MemoryContext<T> : BaseContext<T>, IContext<T> where T : Entity
    {
        public List<T> Items = new List<T>();

        public override void DeleteInternal(T obj)
        {
            Items.Remove(obj);
        }

        public override T GetByPrimaryKeyValueInternal(object key)
        {
            return Items.FirstOrDefault(i => i.GetPrimaryKeyValue() == key);
        }

        public override List<T> GetWithFilterInternal(List<KeyValuePair<string, object>> filters = null)
        {
            if (filters == null || filters.Count == 0)
                return Items;

            List<T> items = new List<T>();
            foreach (T item in Items)
                foreach (KeyValuePair<string, object> filter in filters)
                {
                    object val = item.GetValueByProperty(filter.Key);
                    if (filter.Value.Equals(val))
                        items.Add(item);
                }

            return items;
        }

        public override void InsertInternal(T obj)
        {
            Items.Add(obj);
        }

        public override void UpdateInternal(T obj)
        {
            T item = Items.Where(i => i.GetPrimaryKeyValue() == obj.GetPrimaryKeyValue()).FirstOrDefault();
            item = obj;
        }
    }
}
