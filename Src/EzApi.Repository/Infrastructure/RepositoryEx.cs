using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ez.Repository;
using Repository.Pattern.Ef6;
using TrackableEntities;

namespace CppeDb.Repository
{
    public class RepositoryEx<TEntity> : Repository<TEntity>, IRepositoryEx<TEntity> where TEntity : class, ITrackable
    {

        public RepositoryEx(DbContext context, IUnitOfWorkAsyncEx unitOfWork
            ): base(context, unitOfWork)
        {
        }
        public async Task DeleteFromQueryAsync()
        {
            throw new Exception("Not Implemented yet");
        }

        public async Task UpdateFromQueryAsync(Expression<Func<TEntity,TEntity>> expression)
        {
            throw new Exception("Not Implemented yet");
        }

        public IRepositoryEx<T> GetRepositoryEx<T>() where T : class, ITrackable
        {
            return UnitOfWork.RepositoryAsync<T>() as IRepositoryEx<T>;
            //return base.GetRepository<T>() as IRepositoryEx<T>;
        }
   


        public override void Insert(TEntity entity)
        {
            AuditInsert(entity);
            base.Insert(entity);
        }


        public override void InsertRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Insert(entity);
            }
        }

        public override void InsertGraphRange(IEnumerable<TEntity> entities)
        {
            InsertRange(entities);
        }

        public override void InsertOrUpdateGraph(TEntity entity)
        {
            AuditInsert(entity);
            base.InsertOrUpdateGraph(entity);
        }

        public override void Update(TEntity entity)
        {
            AuditUpdate(entity);
            base.Update(entity);
        }

        public void Detach(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Detached;
        }

        private void AuditInsert(TEntity entity)
        {
            throw new Exception("Not Implemented yet");
        }

        private void AuditUpdate(TEntity entity)
        {
            throw new Exception("Not Implemented yet");
        }
    }
}