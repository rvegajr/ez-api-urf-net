using System;
using System.Threading;
using System.Threading.Tasks;
using Ez.Repository;
using Repository.Pattern.UnitOfWork;
using TrackableEntities;

namespace Ez.Repository
{
    /// <summary>
    /// This is where we can place DbContext operations.
    /// </summary>
    public interface IUnitOfWorkAsyncEx : IUnitOfWorkAsync
    {
        Action<string> Log { get; set; }
        Task<TValue> ExecuteSqlScalarAsync<TValue>(string sql, CancellationToken cancellationToken, params object[] parameters);
        bool IgnoreContainer { get; set; }
        bool InTransaction { get; }

        IRepositoryEx<TEntity> RepositoryEx<TEntity>() where TEntity : class, ITrackable;
        //void BulkInsertAsync(object p);
    }
}