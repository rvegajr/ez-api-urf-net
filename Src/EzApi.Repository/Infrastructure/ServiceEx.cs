using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Linq.Expressions;
using TrackableEntities;

namespace Ez.Repository
{
    public class ServiceEx<TEntity> : Service<TEntity>, IServiceEx<TEntity>
        where TEntity : class, ITrackable
    {
        protected IRepositoryEx<TEntity> _repositoryAsync;
        protected ServiceEx(IRepositoryEx<TEntity> repository) : base(repository)
        {
            _repositoryAsync = repository;
        }
        protected ServiceEx(IRepositoryAsync<TEntity> repository) : base(repository)
        {
            _repositoryAsync = repository as IRepositoryEx<TEntity>;
        }

        /// <summary>
        /// CAUTION: This may be null if not explicitly set, typically from a controller
        /// </summary>
        public IUnitOfWorkAsyncEx UnitOfWork { get; set; }

        public void Update(TEntity entity, Expression<Func<TEntity, bool>> filter)
        {
            base.Update(entity);
        }
    }
}