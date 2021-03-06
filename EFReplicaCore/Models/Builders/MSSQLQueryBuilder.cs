﻿using EFReplicaCore.Enums;
using EFReplicaCore.Interfaces;
using EFReplicaCore.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFReplicaCore.Models.Builders
{
    public class MSSQLQueryBuilder : IQueryBuilder
    {
        public string Table { get; set; }

        public MSSQLQueryBuilder(string table)
        {
            Table = table;
        }

        public MSSQLQueryBuilder()
        { }

        public void SetTable(string Table)
        {
            this.Table = Table;
        }

        private static Dictionary<string, string> placeHolders = new Dictionary<string, string>()
        {
            { "select", "SELECT" },
            { "update", "UPDATE"},
            { "delete", "DELETE"},
            { "from", "FROM" },
            { "where", "WHERE" },
            { "order", "ORDER BY" },
            { "group", "GROUP BY" },
            { "limit", "TOP" },
            { "and", "AND" },
            { "or", "OR" }
        };

        private List<string> reservedWords = new List<string>
        {
            "USER",
            "PASSWORD"
        };

        protected string validatedQuery(string query)
        {
            foreach(string word in reservedWords)
            {
                if (query.ToUpper().Contains(word))
                    query = query.ToUpper().Replace(word, $"[{word}]");
            }

            return query;
        }

        #region filterparser
        public string FilterToWhere(List<ColumnFilter> filters)
        {
            string query = "";
            foreach(ColumnFilter filter in filters)
            {
                if (query.Length > 0)
                    query += $" {placeHolders["and"]} ";

                query += ParseFilter(filter);
            }
            return $"{placeHolders["where"]} {query}";
        }

        public string ParseFilter(ColumnFilter filter)
        {
            switch(filter.Type)
            {
                default:
                case FilterType.Equals:
                    return $"{filter.Column} = '{filter.Value.ToString()}'";
                case FilterType.Like:
                    return $"{filter.Column} LIKE '%{filter.Value.ToString()}%'";
                case FilterType.NotLike:
                    return $"{filter.Column} NOT LIKE '%{filter.Value.ToString()}%'";
            }
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
        #endregion


        public string GetSelectQuery(List<string> selects = null,
            List<ColumnFilter> filters = null,
            List<KeyValuePair<string, string>> order = null,
            List<string> group = null)
        {
            string query = "";

            if (selects == null || selects.Count < 1)
                query += $"{placeHolders["select"]} * from {Table} ";
            else
                query += $"{placeHolders["select"]} {FilterToSelect(selects)} from {Table} ";

            if (filters != null && filters.Count > 0)
                query += FilterToWhere(filters);

            if (order != null && order.Count > 0)
                query += FilterToOrder(order);

            if (group != null && group.Count > 0)
                query += FilterToGroup(group);

            return validatedQuery(query);
        }

        public string GetInsertQuery(List<KeyValuePair<string, object>> pairs)
        {
            string query = $"insert into {Table}(@fields) values (@values)";
            string fields = "";
            string values = "";

            foreach(KeyValuePair<string, object> field in pairs)
            {
                if (!string.IsNullOrWhiteSpace(fields))
                    fields += ",";
                fields += field.Key;

                if (!string.IsNullOrWhiteSpace(values))
                    values += ",";
                values += $"'{field.Value.ToString()}'";
            }

            return validatedQuery(query.Replace("@fields", fields).Replace("@values", values));
        }

        public string GetDeleteQuery(List<ColumnFilter> filters)
        {
            return validatedQuery($"delete from {Table} {FilterToWhere(filters)}");
        }

        public string GetUpdateQuery(List<KeyValuePair<string, object>> pairs, List<ColumnFilter> filters)
        {
            if (filters.Count < 1)
                throw new ArgumentOutOfRangeException("There must be atleast 1 filter");

            string query = $"{placeHolders["update"]} {Table} set @fields @where";
            string fields = "";

            foreach(KeyValuePair<string, object> pair in pairs)
            {
                if (!string.IsNullOrWhiteSpace(fields))
                    fields += ",";
                fields += $"{pair.Key} = '{pair.Value}'";
            }

            return validatedQuery(query.Replace("@fields", fields).Replace("@where", FilterToWhere(filters)));
        }

        public string GetTableExistsQuery(string Table)
        {
            string query = "";
            return query;
        }
    }
}
