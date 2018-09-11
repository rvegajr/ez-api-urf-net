using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Repositories;
using TrackableEntities;

namespace Ez.Repository
{
    public interface IRepositoryEx<TEntity>: IRepositoryAsync<TEntity> where TEntity : class, ITrackable
    {
        Task DeleteFromQueryAsync();
        Task UpdateFromQueryAsync(Expression<Func<TEntity, TEntity>> expression);
        IRepositoryEx<T> GetRepositoryEx<T>() where T : class, ITrackable;
    }
}
