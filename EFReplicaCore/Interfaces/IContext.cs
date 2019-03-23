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
        List<T> GetWithFilter(List<KeyValuePair<string, object>> filters);
    }
}
