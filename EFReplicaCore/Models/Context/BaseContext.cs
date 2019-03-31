using EFReplicaCore.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace EFReplicaCore.Models.Context
{
    public abstract class BaseContext<T> where T : Entity
    {
        public List<T> GetAll()
        {
            try
            {
                return GetWithFilterInternal();
            }
            catch (Exception)
            {
                return new List<T>();
            }
        }

        public List<T> GetWithFilter(List<string> selects = null,
            List<ColumnFilter> filters = null,
            List<KeyValuePair<string, string>> order = null,
            List<string> group = null)
        {
            try
            {
                return GetWithFilterInternal(selects, filters, order, group);
            }
            catch(Exception)
            {
                return new List<T>();
            }
        }
        public abstract List<T> GetWithFilterInternal(List<string> selects = null,
            List<ColumnFilter> filters = null,
            List<KeyValuePair<string, string>> order = null,
            List<string> group = null);

        public T GetByPrimaryKeyValue(object key)
        {
            try
            {
                return GetByPrimaryKeyValueInternal(key);
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        public abstract T GetByPrimaryKeyValueInternal(object key);
        
        public bool Insert(T obj)
        {
            try
            {
                InsertInternal(obj);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public abstract void InsertInternal(T obj);

        public bool Update(T obj)
        {
            try
            {
                UpdateInternal(obj);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public abstract void UpdateInternal(T obj);

        public bool Delete(T obj)
        {
            try
            {
                DeleteInternal(obj);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public abstract void DeleteInternal(T obj);
    }
}
