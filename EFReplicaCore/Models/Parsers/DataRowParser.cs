using EFReplicaCore.Attributes;
using EFReplicaCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EFReplicaCore.Models.Parsers
{
    public class DataRowParser : IParser
    {
        protected T GetObject<T>()
        {
            return (T)Activator.CreateInstance(typeof(T));
        }

        public object Parse<T>(object raw) where T : Entity
        {
            T result = GetObject<T>();

            DataRow dr = (raw as DataRow);
            foreach (DataColumn col in dr.Table.Columns)
            {
                if (result.HasProperty(col.ColumnName) && Attribute.IsDefined(result.GetPropertyByName(col.ColumnName), typeof(Property)))
                {
                    result.SetPropertyByName(col.ColumnName, dr[col]);
                }
            }

            return result;
        }

        public bool TryParse<T>(object raw, out T result) where T : Entity
        {
            try
            {
                result = (T)Parse<T>(raw);
                return true;
            }
            catch (Exception e)
            {
                result = null;
                return false;
            }
        }
    }
}
