using System;
using System.Threading.Tasks;
using EvaLabs.Helper.Constant;
using EvaLabs.Helper.ExtensionMethod;
using EvaLabs.Infrastructure;
using EvaLabs.ViewModels.Common;
using Microsoft.Extensions.Logging;

namespace EvaLabs.Services.BaseService
{
    public abstract class Service<TEntity> : IService where TEntity : class
    {
        private readonly ILogger _logger;
        public readonly IRepository<TEntity> Repository;
        public readonly IUnitOfWork UnitOfWork;


        protected Service(IUnitOfWork unitOfWork, ILogger logger)
        {
            _logger = logger;
            UnitOfWork = unitOfWork;
            Repository = unitOfWork.GetRepository<TEntity>();
        }

        public virtual async Task<Result<TResult>> TryDoAsync<TResult>(Func<Task<Result<TResult>>> func)
        {
            try
            {
                _logger.LogInformation($"TryDo.. {func.GetType().CSharpName()}");

                return await func();
            }
            catch (Exception exception)
            {
                return Result<TResult>.Failed(exception);
            }
        }

        public virtual async Task<Result<TResult>> TryDoAsync<TResult>(int id,
            Func<TEntity, Task<Result<TResult>>> func)
        {
            return await TryDoAsync(async () =>
            {
                if (id == default)
                    return Result<TResult>.Failed(AppValues.InvalidData);

                var entity = await Repository.FindAsync(id);
                if (entity == null)
                    return Result<TResult>.Failed(string.Format(AppValues.NotFound, typeof(TEntity)));

                return await func(entity);
            });
        }
    }
}