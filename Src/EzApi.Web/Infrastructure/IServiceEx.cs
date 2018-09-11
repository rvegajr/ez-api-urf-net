using Repository.Pattern.UnitOfWork;
using Service.Pattern;
using System;
using System.Linq.Expressions;
using TrackableEntities;

namespace Ez.Web
{
    public interface IServiceEx<TEntity> : IService<TEntity> where TEntity : class, ITrackable
    {
        void Update(TEntity entity, Expression<Func<TEntity, bool>> filter);
        IUnitOfWorkAsync UnitOfWork { get; set; }
    }
}