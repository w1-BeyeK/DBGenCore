using EFReplicaCore.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EFReplicaCore.Interfaces
{
    public interface IContext<T>
    {
        bool Delete(T obj);
        bool Update(T obj);
        bool Insert(T obj);
        List<T> GetWithFilter(List<string> selects = null,
            List<ColumnFilter> filters = null,
            List<KeyValuePair<string, string>> order = null,
            List<string> group = null);

        bool CanPersist(T obj);
    }
}
