using CppeDb.Repository;
using Service.Pattern;
using System;
using System.Linq.Expressions;
using TrackableEntities;

namespace Ez.Repository
{
    public interface IServiceEx<TEntity> : IService<TEntity> where TEntity : class, ITrackable
    {
        void Update(TEntity entity, Expression<Func<TEntity, bool>> filter);
        IUnitOfWorkAsyncEx UnitOfWork { get; set; }
    }
}
