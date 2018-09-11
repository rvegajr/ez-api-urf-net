using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using CommonServiceLocator;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using TrackableEntities;

namespace Ez.Repository
{
	public class UnitOfWorkEx : UnitOfWork, IUnitOfWorkAsyncEx
	{
		private readonly DbContext _context;

		/// <summary>
		/// SET TO TRUE to keep away from UnityRegistration problems
		/// THis forces all Repositories to be Returned from local cache that are newed up here instead of using unity container
		/// This is essential IF you are going to consume this unit of work in a separate thread outside the API Session Context / Thread
		/// 
		/// </summary>
		public bool IgnoreContainer { get; set; }

		public bool InTransaction
		{
			get { return Transaction != null; }
		}


		public UnitOfWorkEx(DbContext context) : base(context)
		{
			_context = context;
		}
		

		public virtual Action<string> Log
		{
			get => _context.Database.Log;
			set => _context.Database.Log = value;
		}

		public async Task<TValue> ExecuteSqlScalarAsync<TValue>(string sql, CancellationToken cancellationToken, params object[] parameters)
		{
			return await _context.Database.SqlQuery<TValue>(sql, parameters).SingleOrDefaultAsync(cancellationToken);
		}

		public virtual IRepositoryEx<TEntity> RepositoryEx<TEntity>() where TEntity : class, ITrackable
		{
			return RepositoryAsync<TEntity>() as IRepositoryEx<TEntity>;
		}
		public virtual IRepositoryAsync<TEntity> RepositoryAsync<TEntity>() where TEntity : class, ITrackable
		{
			if (!IgnoreContainer && ServiceLocator.IsLocationProviderSet)
			{
				return ServiceLocator.Current.GetInstance<IRepositoryAsync<TEntity>>();
			}

			if (Repositories == null)
			{
				Repositories = new Dictionary<string, dynamic>();
			}
			var type = typeof(TEntity).Name;

			if (Repositories.ContainsKey(type))
			{
				return (IRepositoryAsync<TEntity>)Repositories[type];
			}
			IRepositoryAsync<TEntity> result = null;
			Type repositoryType = null;
			repositoryType = typeof(Repository<>);
			result = (IRepositoryAsync<TEntity>)Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context, this);
			Repositories.Add(type, result);
			return result;
		}
	}
}