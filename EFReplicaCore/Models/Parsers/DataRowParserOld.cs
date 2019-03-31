using EFReplicaCore.Attributes;
using EFReplicaCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EFReplicaCore.Models.Parsers
{
    //public class DataRowParserOld<T> : IParser where T : Entity
    //{
    //    protected T GetObject()
    //    {
    //        return (T)Activator.CreateInstance(typeof(T));
    //    }

    //    public T Parse(object raw)
    //    {
    //        T result = GetObject();

    //        DataRow dr = (raw as DataRow);
    //        foreach (DataColumn col in dr.Table.Columns)
    //        {
    //            if(result.HasProperty(col.ColumnName) && Attribute.IsDefined(result.GetPropertyByName(col.ColumnName), typeof(Property)))
    //            {
    //                result.SetPropertyByName(col.ColumnName, dr[col]);
    //            }
    //        }
            
    //        return result;
    //    }

    //    public bool TryParse(object raw, out T result)
    //    {
    //        try
    //        {
    //            result = Parse(raw);
    //            return true;
    //        }
    //        catch(Exception e)
    //        {
    //            result = null;
    //            return false;
    //        }
    //    }
    //}
}
