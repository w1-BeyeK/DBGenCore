using EFReplicaCore.Enums;
using EFReplicaCore.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFReplicaCore.Interfaces
{
    public interface IQueryBuilder
    {
        void SetTable(string name);

        string GetSelectQuery(List<string> selects = null,
            List<ColumnFilter> filters = null,
            List<KeyValuePair<string, string>> order = null,
            List<string> group = null);
        string GetInsertQuery(List<KeyValuePair<string, object>> pairs);
        string GetUpdateQuery(List<KeyValuePair<string, object>> pairs, List<ColumnFilter> filters);
        string GetDeleteQuery(List<ColumnFilter> filters);

        string FilterToWhere(List<ColumnFilter> filters);
        string FilterToSelect(List<string> filters);
        string FilterToOrder(List<KeyValuePair<string, string>> filters);
        string FilterToGroup(List<string> filters);

        string ParseFilter(ColumnFilter filter);
    }
}
