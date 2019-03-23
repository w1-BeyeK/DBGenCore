using EFReplicaCore.Enums;
using EFReplicaCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFReplicaCore.Models.Builders
{
    public class MSSQLQueryBuilder : IQueryBuilder
    {
        private string table;

        public MSSQLQueryBuilder(string tableName)
        {
            table = tableName;
        }

        public MSSQLQueryBuilder()
        { }

        private static Dictionary<string, string> placeHolders = new Dictionary<string, string>()
        {
            { "select", "SELECT" },
            { "from", "FROM" },
            { "where", "WHERE" },
            { "order", "ORDER BY" },
            { "group", "GROUP BY" },
            { "limit", "TOP" },
            { "and", "AND" },
            { "or", "OR" }
        };
        
        public string FilterToWhere(List<KeyValuePair<string, object>> filters)
        {
            string query = "";
            foreach(KeyValuePair<string, object> filter in filters)
            {
                if (query.Length > 0)
                    query += $" {placeHolders["and"]} ";

                query += ParseFilter(filter);
            }
            return $"{placeHolders["where"]} {query}";
        }

        public string ParseFilter(KeyValuePair<string, object> filter)
        {
            return $"{filter.Key} = '{filter.Value}'";
        }

        public void SetTable(string table)
        {
            this.table = table;
        }

        public string FilterToSelect(List<string> filters)
        {
            string query = "";
            foreach(string filter in filters)
            {
                if (!string.IsNullOrWhiteSpace(query))
                    query += ",";
                query += filter;
            }
            return query;
        }

        public string FilterToGroup(List<string> filters)
        {
            string query = "";
            foreach (string filter in filters)
            {
                if (!string.IsNullOrWhiteSpace(query))
                    query += ",";
                query += filter;
            }
            return $"{placeHolders["group"]} {query}";
        }

        public string FilterToOrder(List<KeyValuePair<string, string>> filters)
        {
            string query = "";
            foreach(KeyValuePair<string, string> filter in filters)
            {
                if (!string.IsNullOrWhiteSpace(query))
                    query += ",";
                query += $"{filter.Key} {filter.Value}";

            }

            return $"{placeHolders["order"]} {query}";
        }

        public string GetSelectQuery(List<string> selects = null, List<KeyValuePair<string, object>> filters = null, List<KeyValuePair<string, string>> order = null, List<string> group = null)
        {
            string query = "";

            if (selects == null)
                query += $"{placeHolders["select"]} * from {table} ";
            else
                query += $"{placeHolders["select"]} {FilterToSelect(selects)} from {table} ";

            if (filters != null && filters.Count > 0)
                query += FilterToWhere(filters);

            if (order != null && order.Count > 0)
                query += FilterToOrder(order);

            if (group != null && group.Count > 0)
                query += FilterToGroup(group);

            return query;
        }
    }
}
