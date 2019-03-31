using EFReplicaCore.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFReplicaCore.Models
{
    public class Converter
    {
        public List<ColumnFilter> KeyValueToColumnFilters(List<KeyValuePair<string, object>> pairs)
        {
            List<ColumnFilter> result = new List<ColumnFilter>();
            foreach(KeyValuePair<string, object> pair in pairs)
            {
                result.Add(new ColumnFilter(pair.Key, pair.Value));
            }
            return result;
        }
    }
}
