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
        public List<KeyValuePair<string, object>> GetProperties(T obj)
        {
            List<KeyValuePair<string, object>> properties = new List<KeyValuePair<string, object>>();
            PropertyInfo[] props = obj.GetType().GetProperties();

            foreach(PropertyInfo prop in props)
            {
                string name = prop.Name;
                object val = prop.GetValue(prop);

                properties.Add(new KeyValuePair<string, object>(name, val));
            }

            return properties;
        }

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
        public List<T> GetWithFilter(List<KeyValuePair<string, object>> filters)
        {
            try
            {
                return GetWithFilterInternal(filters);
            }
            catch(Exception)
            {
                return new List<T>();
            }
        }
        public abstract List<T> GetWithFilterInternal(List<KeyValuePair<string, object>> filters = null);

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
            catch(Exception)
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
            catch (Exception)
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
            catch (Exception)
            {
                return false;
            }
        }

        public abstract void DeleteInternal(T obj);
    }
}
