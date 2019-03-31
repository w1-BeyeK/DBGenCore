//using EFReplicaCore.Interfaces;
//using EFReplicaCore.Models.Helpers;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;

//namespace EFReplicaCore.Models.Context
//{
//    public class MemoryContext<T> : BaseContext<T>, IContext<T> where T : Entity
//    {
//        public List<T> Items = new List<T>();

//        public override void DeleteInternal(List<ColumnFilter> filters)
//        {
//            throw new NotImplementedException();
//        }

//        public override T GetByPrimaryKeyValueInternal(object key)
//        {
//            return Items.FirstOrDefault(i => i.GetPrimaryKeyValue() == key);
//        }

//        public override List<T> GetWithFilterInternal(List<string> selects = null,
//            List<ColumnFilter> filters = null,
//            List<KeyValuePair<string, string>> order = null,
//            List<string> group = null)
//        {
//            if (filters == null || filters.Count == 0)
//                return Items;

//            List<T> items = new List<T>();
//            //foreach (T item in Items)
//            //foreach (KeyValuePair<string, object> filter in filters)
//            //{
//            //    object val = item.GetValueByProperty(filter.Key);
//            //    if (filter.Value.Equals(val))
//            //        items.Add(item);
//            //}

//            return items;
//        }

//        public override void InsertInternal(T obj)
//        {
//            Items.Add(obj);
//        }

//        public bool Update(T newObj, T oldObj)
//        {
//            throw new NotImplementedException();
//        }

//        public override void UpdateInternal(T obj)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
