using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using EvaLabs.Domain.Models;
using EvaLabs.Helper.ExtensionMethod;
using Microsoft.EntityFrameworkCore;

namespace EvaLabs.Services.ExtensionMethod
{
    public static class DbContextExtensions
    {
        public static async Task<TEntity> AddOrUpdateAsync<TEntity, TProperty, T>(
            this DbContext context,
            TEntity entity,
            Expression<Func<TEntity, TProperty>> primaryKey,
            Action<TEntity, T> action,
            CancellationToken cancellationToken = default
        )
            where TEntity : class
        {
            var entry = context.Entry(entity);

            if (entry.State != EntityState.Detached) return entity;

            var predicate = WhereExpression(entity, primaryKey);
            var dbObject = await context.Set<TEntity>().FirstOrDefaultAsync(predicate, cancellationToken);
            if (dbObject != null)
            {
                var arg2 = dbObject.To<T>();
                action.Invoke(entity, arg2);
                entry.State = EntityState.Modified;
            }
            else
            {
                entry.State = EntityState.Added;
            }

            return entity;
        }


        public static async Task<TEntity> AddOrUpdateAsync<TEntity>(
            this DbContext context,
            TEntity entity,
            CancellationToken cancellationToken = default
        )
            where TEntity : BaseEntity
        {
            return await context.AddOrUpdateAsync(entity, x => x.Id, cancellationToken);
        }


        public static async Task<TEntity> AddOrUpdateAsync<TEntity, TProperty>(
            this DbContext context,
            TEntity entity,
            Expression<Func<TEntity, TProperty>> primaryKey,
            CancellationToken cancellationToken = default
        )
            where TEntity : BaseEntity
        {
            var entry = context.Entry(entity);

            if (entry.State != EntityState.Detached) return entity;

            var predicate = WhereExpression(entity, primaryKey);
            var dbObject = await context.Set<TEntity>().FirstOrDefaultAsync(predicate, cancellationToken);
            context.Entry(entity).State = dbObject != null ? EntityState.Modified : EntityState.Added;

            return entity;
        }


        public static async Task<TEntity> AddOrUpdateAuditAsync<TEntity>(
            this DbContext context,
            TEntity entity,
            CancellationToken cancellationToken = default
        )
            where TEntity : Auditable
        {
            return await context.AddOrUpdateAsync(entity, x => x.Id, cancellationToken);
        }


        public static async Task<TEntity> AddOrUpdateAuditAsync<TEntity, TProperty>(
            this DbContext context,
            TEntity entity,
            Expression<Func<TEntity, TProperty>> primaryKey,
            CancellationToken cancellationToken = default
        )
            where TEntity : Auditable
        {
            var entry = context.Entry(entity);

            if (entry.State != EntityState.Detached) return entity;

            var predicate = WhereExpression(entity, primaryKey);
            var dbObject = await context.Set<TEntity>().FirstOrDefaultAsync(predicate, cancellationToken);
            if (dbObject != null)
            {
                entity.CreatorId = dbObject.CreatorId;
                entity.CreationDate = dbObject.CreationDate;
                context.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                context.Entry(entity).State = EntityState.Added;
            }

            return entity;
        }

        public static async Task BulkInsertAsync<TEntity>(
            this DbContext context,
            IEnumerable<TEntity> items,
            CancellationToken cancellationToken
        )
            where TEntity : class
        {
            var detectChanges = context.ChangeTracker.AutoDetectChangesEnabled;
            try
            {
                context.ChangeTracker.AutoDetectChangesEnabled = false;
                var set = context.Set<TEntity>();

                foreach (var item in items) await set.AddAsync(item, cancellationToken);
            }
            finally
            {
                context.ChangeTracker.AutoDetectChangesEnabled = detectChanges;
            }
        }


        /// <summary>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="entity"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        private static Expression<Func<TEntity, bool>> WhereExpression<TEntity, TProperty>(TEntity entity,
            Expression<Func<TEntity, TProperty>> property)
        {
            if (property == null)
                throw new ArgumentNullException(nameof(property));

            var propertyInfo = GetPropertyInfo(property);
            var keyValue = propertyInfo.GetValue(entity, null);
            var entityType = propertyInfo.DeclaringType
                             ?? throw new ArgumentNullException(nameof(property));

            var parameter = Expression.Parameter(entityType, typeof(TEntity).Name.ToLower());
            var comparison =
                Expression.Equal(Expression.Property(parameter, propertyInfo), Expression.Constant(keyValue));
            var expression = Expression.Lambda<Func<TEntity, bool>>(comparison, parameter);
            return expression;
        }

        /// <summary>
        ///     Gets the corresponding <see cref="PropertyInfo" /> from an <see cref="Expression" />.
        /// </summary>
        /// <param name="property">The expression that selects the property to get info on.</param>
        /// <returns>The property info collected from the expression.</returns>
        /// <exception cref="ArgumentNullException">When <paramref name="property" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">The expression doesn't indicate a valid property."</exception>
        private static PropertyInfo GetPropertyInfo<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> property)
        {
            if (property == null)
                throw new ArgumentNullException(nameof(property));

            return property.Body switch
            {
                UnaryExpression {Operand: MemberExpression memberExp} => (PropertyInfo) memberExp
                    .Member,
                MemberExpression memberExp => (PropertyInfo) memberExp.Member,
                _ => throw new ArgumentException($"The expression doesn't indicate a valid property. [ {property} ]")
            };
        }
    }
}