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
        private readonly IQueryBuilder builder;
        private readonly IHandler handler;

        private readonly IParser<T> parser;

        private readonly string connection;

        public MSSQLContext(IQueryBuilder builder, IHandler handler, IParser<T> parser, string conn)
        {
            this.builder = builder;
            this.handler = handler;
            this.parser = parser;
            connection = conn;
        }

        public MSSQLContext(string conn): this(new MSSQLQueryBuilder(), new MSSQLDatabaseHandler(conn), new DataRowParser<T>(), conn)
        {
            this.builder.SetTable("Users");
        }

        protected string GetTableName(T obj)
        {
            return obj.GetType().Name;
        }

        public override void DeleteInternal(T obj)
        {

            string query = builder.GetDeleteQuery(obj.ToKeyValue());
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
            string query = builder.GetInsertQuery(obj.ToKeyValue());
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
            throw new NotImplementedException();
        }

        public override T GetByPrimaryKeyValueInternal(object key)
        {
            throw new NotImplementedException();
        }
    }
}
