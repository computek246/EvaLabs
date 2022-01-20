//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using AutoMapper;
//using EvaLabs.Domain.Models;
//using EvaLabs.Helper.Constant;
//using EvaLabs.Infrastructure;
//using EvaLabs.Infrastructure.Collections;
//using EvaLabs.Infrastructure.SingletonFactory;
//using EvaLabs.Services.BaseService;
//using EvaLabs.Services.ExtensionMethod;
//using EvaLabs.Services.Interfaces;
//using EvaLabs.ViewModels.City;
//using EvaLabs.ViewModels.Common;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;

namespace EvaLabs.Services.Implementations
{
    public class CityService //: Service<City>, ICityService
    {
        //private readonly IMapper _mapper;
        //private readonly ILogger<CityService> _logger;

        //public CityService(
        //    IMapper mapper,
        //    IUnitOfWork unitOfWork,
        //    ILogger<CityService> logger
        //) : base(unitOfWork, logger)
        //{
        //    _mapper = mapper;
        //    _logger = logger;
        //}

        //public async Task<Result<IList<CityVm>>> ListAllAsync(CancellationToken cancellationToken)
        //{
        //    var instance = SingletonList<City>.Instance;
        //    return await TryDoAsync(async () =>
        //    {
        //        var cities = await Repository.GetAll()
        //            .Include(e => e.Region)
        //            .Include(e => e.Country)
        //            .Select(city => _mapper.Map<CityVm>(city))
        //            .ToListAsync(cancellationToken);
        //        return Result<IList<CityVm>>.Success(cities);
        //    });
        //}

        //public async Task<Result<IPagedList<CityVm>>> ListAsync(FilterVm filter, CancellationToken cancellationToken)
        //{
        //    return await TryDoAsync(async () =>
        //    {
        //        var cities = await Repository.GetAll()
        //            .Include(e => e.Region)
        //            .Include(e => e.Country)
        //            .Select(city => _mapper.Map<CityVm>(city))
        //            .ToPagedListAsync(filter.PageIndex, filter.PageSize,
        //                cancellationToken: cancellationToken);
        //        return Result<IPagedList<CityVm>>.Success(cities);
        //    });
        //}

        //public async Task<Result<CityVm>> CreateOrUpdateAsync(CityVm model, CancellationToken cancellationToken)
        //{
        //    return await TryDoAsync(async () =>
        //    {
        //        if (model == null)
        //            return Result<CityVm>.Failed(AppValues.InvalidData);
        //        if (model.HasErrors)
        //            return Result<CityVm>.Failed(model.Errors);

        //        var entity = _mapper.Map<City>(model);

        //        await Repository.AddOrUpdateAsync<City, Auditable>(entity, x => x.Id, (x, y) =>
        //        {
        //            x.CreatorId = y.CreatorId;
        //            x.CreationDate = y.CreationDate;
        //        }, cancellationToken);

        //        await UnitOfWork.SaveChangesAsync();
        //        var cityVm = _mapper.Map<CityVm>(entity);
        //        return Result<CityVm>.Success(cityVm);
        //    });
        //}

        //public async Task<Result<CityVm>> GetByIdAsync(int id, CancellationToken cancellationToken)
        //{
        //    return await TryDoAsync(id, async city =>
        //    {
        //        await Task.CompletedTask;
        //        var cityVm = _mapper.Map<CityVm>(city);
        //        return Result<CityVm>.Success(cityVm);
        //    });
        //}

        //public async Task<Result<bool>> DeleteAsync(int id, CancellationToken cancellationToken)
        //{
        //    return await TryDoAsync(id, async city =>
        //    {
        //        Repository.Delete(city);
        //        await UnitOfWork.SaveChangesAsync();
        //        return Result<bool>.Success(true);
        //    });
        //}

        //public async Task<Result<bool>> ToggleEnableProp(int id, CancellationToken cancellationToken)
        //{
        //    return await TryDoAsync(id, async city =>
        //    {
        //        city.IsActive = !city.IsActive;
        //        Repository.Update(city);
        //        await UnitOfWork.SaveChangesAsync();
        //        return Result<bool>.Success(true);
        //    });
        //}
    }
}