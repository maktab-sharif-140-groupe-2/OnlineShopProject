using Microsoft.EntityFrameworkCore;
using OnlineShopProject.WebApi.Domain.Common.Paginations;
using OnlineShopProject.WebApi.Domain.Entities.Abstractions;
using OnlineShopProject.WebApi.Infrastructure.Data;
using OnlineShopProject.WebApi.Infrastructure.Repositories.Interface;
using System.Linq.Expressions;

namespace OnlineShopProject.WebApi.Infrastructure.Repositories.Implementation;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _applicationDb;

    private readonly DbSet<T> _repo;

    public GenericRepository(ApplicationDbContext applicationDb)
    {
        _applicationDb = applicationDb;

        _repo = applicationDb.Set<T>();
    }

    public async Task AddEntityAsync(T entity)
    {
       await _repo.AddAsync(entity);
    }

    public async Task DeleteAsync(T entity,Guid deleterId)
    {
        entity.SoftDelete(deleterId);
    }

    public async Task<bool> ExsitingAsync(Expression<Func<T, bool>> condition)
    {
        var result= await _repo.FirstOrDefaultAsync(condition);
        return result != null;
    }

    public async Task<T?> GetEntityByIdAsync(Guid entityId)
    {
        return await _repo.FirstOrDefaultAsync(e=> e.Id==entityId);
        
    }

    public async Task<Pagination<TResult>>  QueryAsync<TResult>(Expression<Func<T, bool>> condition, Expression<Func<T, TResult>> perdicate, int page = 1, int pageSize = 10)
    {
        pageSize= pageSize>50?50:pageSize;

        var query = _repo.AsQueryable().AsNoTracking();

          query = query
            .Where(condition);

        var totaldata=query.Count();

        var data = await query.Select(perdicate)
            .Take(pageSize).
            Skip((page-1)*pageSize)
            .ToListAsync();

        return Pagination<TResult>.GetPagination(data, page, pageSize, totaldata);
      
    }
}
