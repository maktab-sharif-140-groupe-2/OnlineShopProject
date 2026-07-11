using OnlineShopProject.WebApi.Domain.Common.Paginations;
using OnlineShopProject.WebApi.Domain.Entities.Abstractions;
using System.Linq.Expressions;

namespace OnlineShopProject.WebApi.Infrastructure.Repositories.Interface;

public interface IGenericRepository<T> where T : BaseEntity
{

    Task AddEntityAsync(T entity);

    Task<T?> GetEntityByIdAsync(Guid entityId);

    Task<bool> ExsitingAsync(Expression<Func<T, bool>> condition);

    Task<Pagination<TResult>> QueryAsync<TResult>(Expression<Func<T, bool>> condition
        , Expression<Func<T, TResult>> perdicate,int page=1,int pageSize=10);

    Task DeleteAsync(T entity, Guid deleterId);

}
