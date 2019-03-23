using EFReplicaCore.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFReplicaCore.Interfaces
{
    public interface IQueryBuilder
    {
        void SetTable(string name);

        string GetSelectQuery(List<string> selects = null,
            List <KeyValuePair<string, object>> filters = null,
            List<KeyValuePair<string, string>> order = null,
            List<string> group = null);
        string FilterToWhere(List<KeyValuePair<string, object>> filters);
        string FilterToSelect(List<string> filters);
        string FilterToOrder(List<KeyValuePair<string, string>> filters);
        string FilterToGroup(List<string> filters);

        string ParseFilter(KeyValuePair<string, object> filter);
    }
}
