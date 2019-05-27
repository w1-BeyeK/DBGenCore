using EFReplicaCore.Enums;
using EFReplicaCore.Interfaces;
using EFReplicaCore.Models.Builders;
using EFReplicaCore.Models.Handlers;
using EFReplicaCore.Models.Helpers;
using EFReplicaCore.Models.Parsers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EFReplicaCore.Models.Context
{
    public class MSSQLContext<T> : BaseContext<T>, IContext<T> where T : Entity
    {
        protected readonly IQueryBuilder builder;
        protected readonly IHandler handler;

        protected readonly IParser parser;

        private readonly Converter converter = new Converter();

        public MSSQLContext(IQueryBuilder builder, IHandler handler, IParser parser)
        {
            this.builder = builder;
            this.handler = handler;
            this.parser = parser;
        }

        public MSSQLContext(string conn): this(new MSSQLQueryBuilder(), new MSSQLDatabaseHandler(conn), new DataRowParser())
        { }

        protected T GetObject<T>()
        {
            return (T)Activator.CreateInstance(typeof(T));
        }

        protected string GetTableName(T obj)
        {
            return obj.GetType().Name;
        }

        public override void DeleteInternal(T obj)
        {
            if (string.IsNullOrEmpty(builder.Table))
                builder.Table = GetObject<T>().GetType().Name;

            string query =  builder.GetDeleteQuery(converter.KeyValueToColumnFilters(obj.GetPrimaryKeyValue()));
            try
            {
                bool success = handler.ExecuteCommand(query);
            }
            catch (Exception e)
            {
                // Something bad happened
            }
        }

        public override List<T> GetWithFilterInternal(List<string> selects = null,
            List<ColumnFilter> filters = null,
            List<KeyValuePair<string, string>> order = null,
            List<string> group = null)
        {
            if (string.IsNullOrEmpty(builder.Table))
                builder.Table = GetObject<T>().GetType().Name;

            try
            {
                string query = builder.GetSelectQuery(selects, filters, order, group);
                object result = handler.ExecuteSelect(query);

                List<T> items = new List<T>();
                foreach (DataRow dr in (result as DataTable).Rows)
                {
                    if (parser.TryParse(dr, out T obj))
                        items.Add(obj);
                }
                
                return items;
            }
            catch(Exception e)
            {
                return default(List<T>);
            }
        }

        public override void InsertInternal(T obj)
        {
            if (string.IsNullOrEmpty(builder.Table))
                builder.Table = GetObject<T>().GetType().Name;

            string query = builder.GetInsertQuery(obj.ToKeyValue(false));
            try
            {
                bool success = handler.ExecuteCommand(query);
            }
            catch(Exception e)
            {
                // Something bad happened
            }
        }

        public override void UpdateInternal(T obj)
        {
            if (string.IsNullOrEmpty(builder.Table))
                builder.Table = GetObject<T>().GetType().Name;

            string query = builder.GetUpdateQuery(obj.ToKeyValue(false), converter.KeyValueToColumnFilters(obj.GetPrimaryKeyValue()));
            try
            {
                bool success = handler.ExecuteCommand(query);
            }
            catch(Exception e)
            {
                // Something bad happened
            }
        }

        public override T GetByPrimaryKeyValueInternal(object key)
        {
            throw new NotImplementedException();
        }

        public bool CanPersist(T obj)
        {
            string query = builder.GetTableExistsQuery(obj.GetType().Name + "s");
            return false;
        }
    }
}
