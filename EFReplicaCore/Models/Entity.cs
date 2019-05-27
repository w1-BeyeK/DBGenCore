using EFReplicaCore.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace EFReplicaCore.Models
{
    /// <summary>
    /// Class which is primarily used for this ORM
    /// </summary>
    /// <example>
    /// <list type="bullet">
    /// <item>
    /// <description>Maps all properties to key-value pairs
    /// <code>
    /// Entity entity;
    /// entity.GetProperties();
    /// </code>
    /// </description>
    /// </item>
    /// <item>
    /// <description>Gets primary key value based on defined attributes
    /// <code>
    /// Entity entity;
    /// entity.GetPrimaryKeyValue();
    /// </code>
    /// </description>
    /// </item>
    /// <item>
    /// <description>Can get and set property by name
    /// <code>
    /// Entity entity;
    /// var prop = entity.GetPropertyByName("Name"); // results in PropertyInfo object
    /// entity.SetPropertyByName("Name, "Random person...");
    /// </code>
    /// </description>
    /// </item>
    /// </list>
    /// </example>
    public abstract class Entity
    {
        public virtual bool IsValid()
        {
            return true;
        }

        public List<KeyValuePair<string, object>> GetProperties()
        {
            List<KeyValuePair<string, object>> properties = new List<KeyValuePair<string, object>>();
            PropertyInfo[] props = GetType().GetProperties();

            foreach (PropertyInfo prop in props)
            {
                string name = prop.Name;
                object val = prop.GetValue(this, null);

                properties.Add(new KeyValuePair<string, object>(name, val));
            }

            return properties;
        }

        public List<KeyValuePair<string, object>> ToKeyValue(bool includeKeys = true)
        {
            List<KeyValuePair<string, object>> items = new List<KeyValuePair<string, object>>();
            foreach (PropertyInfo prop in GetType().GetProperties())
            {
                if(Attribute.IsDefined(prop, typeof(Property)) &&
                    !Attribute.IsDefined(prop, typeof(PrimaryKey))
                    )
                    items.Add(new KeyValuePair<string, object>(prop.Name, prop.GetValue(this, null)));
            }

            if (includeKeys)
                items.AddRange(GetPrimaryKeyValue());

            return items;
        }

        public List<KeyValuePair<string, object>> GetPrimaryKeyValue()
        {
            // Define a list cause there could be a clustered primary key
            List<KeyValuePair<string, object>> result = new List<KeyValuePair<string, object>>();
            foreach (PropertyInfo prop in GetPropsWithAttribute(typeof(PrimaryKey)))
            {
                result.Add(new KeyValuePair<string, object>(prop.Name, prop.GetValue(this, null)));
            }
            return result;
        }

        public PropertyInfo GetPropertyByName(string propName, bool checkProperty = false)
        {
            var props = GetType().GetProperties();

            foreach(PropertyInfo prop in props)
            {
                if (prop.Name.ToLower() == propName.ToLower() || 
                    (checkProperty && Attribute.IsDefined(prop, typeof(Property)) && 
                    ((Property)Attribute.GetCustomAttributes(prop, typeof(Property))[0]).PropertyName == propName))
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
                .FindAll(t => Attribute.IsDefined(t, type));
        }
    }
}
