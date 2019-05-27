using EFReplicaCore.Attributes;
using EFReplicaCore.Enums;
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
                if (string.IsNullOrEmpty(dr[col].ToString()))
                    continue;

                if (result.HasProperty(col.ColumnName) && Attribute.IsDefined(result.GetPropertyByName(col.ColumnName), typeof(Property)))
                {
                    DataType type = ((Property)Attribute.GetCustomAttributes(result.GetPropertyByName(col.ColumnName), typeof(Property))[0]).DataType;
                    object value;
                    switch (type)
                    {
                        case DataType.DateTime:
                            value = DateTime.Parse(dr[col].ToString());
                            break;
                        default:
                            value = dr[col];
                            break;
                    }
                    result.SetPropertyByName(col.ColumnName, value);
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
