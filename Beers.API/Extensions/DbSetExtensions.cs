﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Beers.API.Extensions
{
    public static class DbSetExtension
    {
        public static void AddOrUpdate<T>(this DbSet<T> dbSet, params T[] data) where T : class
        {
            var context = dbSet.GetContext();
            var ids = context.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties.Select(x => x.Name);

            var t = typeof(T);
            List<PropertyInfo> keyFields = new List<PropertyInfo>();

            foreach (var propt in t.GetProperties())
            {
                var keyAttr = ids.Contains(propt.Name);
                if (keyAttr)
                {
                    keyFields.Add(propt);
                }
            }
            if (keyFields.Count <= 0)
            {
                throw new Exception($"{t.FullName} does not have a KeyAttribute field. Unable to exec AddOrUpdate call.");
            }

            foreach (var item in data)
            {
                var entities = dbSet.IgnoreQueryFilters().AsNoTracking().ToList();
                foreach (var keyField in keyFields)
                {
                    var keyVal = keyField.GetValue(item);
                    entities = entities.Where(p => p.GetType().GetProperty(keyField.Name).GetValue(p).Equals(keyVal)).ToList();
                }
                var dbVal = entities.FirstOrDefault();
                if (dbVal != null)
                {
                    context.Entry(dbVal).CurrentValues.SetValues(item);
                    context.Entry(dbVal).State = EntityState.Modified;
                    return;
                }
                dbSet.Add(item);
            }
        }

        public static DbContext GetContext<TEntity>(this DbSet<TEntity> dbSet) where TEntity : class
        {
            return (DbContext)dbSet
                .GetType().GetTypeInfo()
                .GetField("_context", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(dbSet);
        }
    }
}
