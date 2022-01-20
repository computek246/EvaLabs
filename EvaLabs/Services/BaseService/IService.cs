using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EvaLabs.Domain.Models.Interfaces;
using EvaLabs.ViewModels.Common;

namespace EvaLabs.Services.BaseService
{
    public interface IService
    {
        Task<Result<TResult>> TryDoAsync<TResult>(Func<Task<Result<TResult>>> func);

    }


    public interface IService<in TKey, TModel, in TFilter> : IService
        where TKey : IEquatable<TKey>
        where TModel : IEntity<TKey>, new()
        where TFilter : class
    {
        public Task<IEnumerable<TModel>> ListAsync(TFilter source, CancellationToken cancellationToken);
        public Task<TModel> GetByIdAsync(TKey id, CancellationToken cancellationToken);
        public Task<TModel> CreateOrUpdateAsync(TModel resultObj, CancellationToken cancellationToken);
        public Task<bool> DeleteAsync(TKey id, CancellationToken cancellationToken);
        public Task<bool> ToggleEnableProp(TKey id, CancellationToken cancellationToken);
    }
}