﻿using EFReplicaCore.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace EFReplicaCore.Models
{
    public class Entity
    {
        [Property]
        [AutoIncrement]
        public long Id { get; set; }

        public bool IsValid()
        {
            return true;
        }

        public List<KeyValuePair<string, object>> ToKeyValue()
        {
            List<KeyValuePair<string, object>> items = new List<KeyValuePair<string, object>>();
            foreach (PropertyInfo prop in GetType().GetProperties())
            {
                if(Attribute.IsDefined(prop, typeof(Property)) && !Attribute.IsDefined(prop, typeof(AutoIncrement)))
                    items.Add(new KeyValuePair<string, object>(prop.Name, prop.GetValue(this, null)));
            }
            return items;
        }

        public object GetPrimaryKeyValue()
        {
            // Define a list cause there could be a clustered primary key
            List<object> result = new List<object>();
            foreach (PropertyInfo prop in GetPropsWithAttribute(typeof(Required)))
            {
                result.Add(prop.GetValue(prop, null));
            }
            return result;
        }

        public PropertyInfo GetPropertyByName(string propName)
        {
            var props = GetType().GetProperties();

            foreach(PropertyInfo prop in props)
            {
                if (prop.Name.ToLower() == propName.ToLower())
                    return prop;
            }
            return null;
        }

        public void SetPropertyByName(string propName, object value)
        {
            PropertyInfo prop = GetPropertyByName(propName);
            prop.SetValue(this, value);
        }

        public bool HasProperty(string propName)
        {
            PropertyInfo prop = GetPropertyByName(propName);

            return prop != null;
        }

        public object GetValueByProperty(string propName)
        {
            var prop = GetPropertyByName(propName);
            return prop.GetValue(this, null);
        }
        
        private IEnumerable<PropertyInfo> GetPropsWithAttribute(Type type)
        {
            // Create new list based on all properties
            return new List<PropertyInfo>(GetType().GetProperties())
                // Then search for the required attribute
                .FindAll(t => Attribute.IsDefined(t, typeof(Type)));
        }
    }
}
