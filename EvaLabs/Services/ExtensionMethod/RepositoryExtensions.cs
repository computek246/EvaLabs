using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using EvaLabs.Domain.Models;
using EvaLabs.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EvaLabs.Services.ExtensionMethod
{
    public static class RepositoryExtensions
    {

        public static async Task<TEntity> AddOrUpdateAsync<TEntity>(
            this IRepository<TEntity> repository,
            TEntity entity,
            CancellationToken cancellationToken = default)
            where TEntity : BaseEntity
        {
            return await repository.AddOrUpdateAsync(entity, x => x.Id, cancellationToken);
        }


        public static async Task<TEntity> AddOrUpdateAsync<TEntity>(
            this IRepository<TEntity> repository,
            TEntity entity,
            Expression<Func<TEntity, dynamic>> primaryKey,
            CancellationToken cancellationToken = default)
            where TEntity : class
        {
            var repo = (Repository<TEntity>)repository;
            var dbContext = repo.DbContext;
            var entry = dbContext.Entry(entity);
            var entityState = entry.State;
            if (entityState != EntityState.Detached)
                return entity;

            var predicate = WhereExpression(entity, primaryKey);
            var dbObject = await dbContext.Set<TEntity>().FirstOrDefaultAsync(predicate, cancellationToken);
            entry.State = dbObject != null ? EntityState.Modified : EntityState.Added;

            return entity;
        }


        public static async Task<TEntity> AddOrUpdateAsync<TEntity, TAudit>(
            this IRepository<TEntity> repository,
            TEntity entity,
            Expression<Func<TEntity, dynamic>> primaryKey,
            Action<TEntity, TAudit> action,
            CancellationToken cancellationToken = default)
            where TEntity : class, TAudit
            where TAudit : class
        {
            var repo = (Repository<TEntity>)repository;
            var dbContext = repo.DbContext;
            var entry = dbContext.Entry(entity);
            var entityState = entry.State;
            if (entityState != EntityState.Detached)
                return entity;

            var predicate = WhereExpression(entity, primaryKey);
            var dbObject = await dbContext.Set<TEntity>().FirstOrDefaultAsync(predicate, cancellationToken);
            if (dbObject != null)
            {
                var arg2 = Mapper.Map<TEntity, TAudit>(dbObject);
                action.Invoke(entity, arg2);
                entry.State = EntityState.Modified;
            }
            else
            {
                entry.State = EntityState.Added;
            }

            return entity;
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
                UnaryExpression { Operand: MemberExpression memberExp } => (PropertyInfo)memberExp
                    .Member,
                MemberExpression memberExp => (PropertyInfo)memberExp.Member,
                _ => throw new ArgumentException($"The expression doesn't indicate a valid property. [ {property} ]")
            };
        }


    }
}