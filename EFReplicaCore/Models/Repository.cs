using EFReplicaCore.Interfaces;
using EFReplicaCore.Models.Context;
using EFReplicaCore.Models.Exceptions;
using EFReplicaCore.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EFReplicaCore.Models
{
    /// <summary>
    /// Generic repository used to map objects to the database - and vice versa
    /// </summary>
    /// <typeparam name="T">Used model type, must inherit from Entity</typeparam>
    /// <example>
    /// To make use of this class you need at least 1 model inheriting from
    /// entity. Which you can then pass along in the following way:
    /// <code>
    /// Repository&lt;User&gt; userRepository
    ///     = new Repository&lt;User&gt;();
    /// </code>
    /// <para>
    /// The Repository class knows only the basic CRUD methods, which you can override
    /// and update as you like.
    /// <code>
    /// void Insert(T obj);
    /// void Update(T obj);
    /// void Delete(T obj);
    /// T GetById(long id);
    /// List&lt;T&gt; GetAll();
    /// </code>
    /// </para>
    /// </example>
    public class Repository<T> where T : Entity
    {
        private readonly IContext<T> context;

        public Repository(IContext<T> context)
        {
            this.context = context;
        }

        public void Insert(T obj)
        {
            if (obj.IsValid())
                this.context.Insert(obj);
            else
                throw new InvalidModelException("Model is invalid");
        }

        public void Update(T obj)
        {
            if (obj.IsValid())
                this.context.Update(obj);
            else
                throw new InvalidModelException("Model is invalid");
        }

        public void Delete(T obj)
        {
            if (obj.IsValid())
            {
                this.context.Delete(obj);
            }
            else
                throw new InvalidModelException("Model is invalid");
        }

        public void Delete(long id)
        {
            Delete(GetWithFilter(filters:
                new List<ColumnFilter>()
                {
                    new ColumnFilter("Id", id)
                }).FirstOrDefault());
        }

        public List<T> GetWithFilter(List<string> selects = null,
            List<ColumnFilter> filters = null,
            List<KeyValuePair<string, string>> order = null,
            List<string> group = null)
        {
            return this.context.GetWithFilter(selects, filters, order, group);
        }

        public T GetById(long id)
        {
            return GetWithFilter(filters: 
                new List<ColumnFilter>()
                {
                    new ColumnFilter("Id", id)
                }).FirstOrDefault();
        }
    }
}
