using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EvaLabs.Infrastructure.Collections;
using EvaLabs.Services.BaseService;
using EvaLabs.ViewModels.City;
using EvaLabs.ViewModels.Common;

namespace EvaLabs.Services.Interfaces
{
    public interface ICityService : IService
    {
        Task<Result<IList<CityVm>>> ListAllAsync(CancellationToken cancellationToken);
        Task<Result<IPagedList<CityVm>>> ListAsync(FilterVm filter, CancellationToken cancellationToken);
        Task<Result<CityVm>> CreateOrUpdateAsync(CityVm model, CancellationToken cancellationToken);
        Task<Result<CityVm>> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<Result<bool>> DeleteAsync(int id, CancellationToken cancellationToken);
        Task<Result<bool>> ToggleEnableProp(int id, CancellationToken cancellationToken);
    }
}