using System;
using System.Linq.Expressions;
using TrackableEntities;
using Repository.Pattern.Repositories;
using System.Threading.Tasks;
using Unity.Builder;

namespace Ez.Web
{
    public interface IRepositoryEx<TEntity> : IRepositoryAsync<TEntity> where TEntity : class, ITrackable
    {
        Task DeleteFromQueryAsync();
        Task UpdateFromQueryAsync(Expression<Func<TEntity, TEntity>> expression);
        Task UpdateFromQueryAsync(Expression<Func<TEntity, TEntity>> expression, Action<BuildOperation> options);
        IRepositoryEx<T> GetRepositoryEx<T>() where T : class, ITrackable;
    }
}