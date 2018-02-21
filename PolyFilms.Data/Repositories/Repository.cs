using System;
using System.Collections.Generic;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using PolyFilms.Data.Entity;
using System.Linq;
using System.Reflection;

namespace PolyFilms.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext Context;

        protected DbSet<T> DbSet;

        public Repository(PolyFilmsContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        public void Insert(T entity)
        {
            Context.Set<T>().Add(entity);
            Save();
        }

        public void Delete(object key)
        {
            T existing = DbSet.Find(key);
            Context.Set<T>().Remove(existing);
            Save();
        }

        public IQueryable<T> GetAll()
        {
            return DbSet;
        }

        public T GetById<TKey>(TKey id)
        {
            return DbSet.Find(id);
        }

        public void Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Save();
        }

        private void Save()
        {
            int result = Context.SaveChangesAsync().Result;
        }

        public void Update(T entity, PolyFilmsContext context)
        {
            context.Entry(entity).State = EntityState.Modified;
        }
    }

    public static class Extensions
    {
        public static DbCommand LoadStoredProc(this DbContext context, string storedProcName)
        {
            var cmd = context.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = storedProcName;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            return cmd;
        }

        public static DbCommand WithSqlParam(this DbCommand cmd, string paramName, object paramValue)
        {
            if (string.IsNullOrEmpty(cmd.CommandText))
                throw new InvalidOperationException("Call LoadStoredProc before using this method");

            var param = cmd.CreateParameter();
            param.ParameterName = paramName;
            param.Value = paramValue;
            cmd.Parameters.Add(param);
            return cmd;
        }

        private static IList<T> MapToList<T>(this DbDataReader dr)
        {
            var objList = new List<T>();
            var props = typeof(T).GetRuntimeProperties();

            var colMapping = dr.GetColumnSchema()
                .Where(x => props.Any(y => y.Name.ToLower() == x.ColumnName.ToLower()))
                .ToDictionary(key => key.ColumnName.ToLower());

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    T obj = Activator.CreateInstance<T>();
                    foreach (var prop in props)
                    {
                        var val = dr.GetValue(colMapping[prop.Name.ToLower()].ColumnOrdinal.Value);
                        prop.SetValue(obj, val == DBNull.Value ? null : val);
                    }
                    objList.Add(obj);
                }
            }
            return objList;
        }

        public static IList<T> ExecuteStoredProc<T>(this DbCommand command)
        {
            using (command)
            {
                if (command.Connection.State == System.Data.ConnectionState.Closed)
                    command.Connection.Open();
                try
                {
                    using (var reader = command.ExecuteReader())
                    {
                        return reader.MapToList<T>();
                    }
                }
                finally
                {
                    command.Connection.Close();
                }
            }
        }
    }
}
