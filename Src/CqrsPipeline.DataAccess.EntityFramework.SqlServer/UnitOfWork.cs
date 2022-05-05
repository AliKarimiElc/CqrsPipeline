using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CqrsPipeline.DataAccess.EntityFramework.SqlServer
{
    /// <summary>
    /// Default implementation of IUnitOfWork for Entity framework core with sql server database
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public abstract class UnitOfWork<TDbContext> : IUnitOfWork where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dbContext"></param>
        public UnitOfWork(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public IDbConnection Connection => _dbContext.Database.GetDbConnection();

        /// <inheritdoc />
        public IDbTransaction Transaction
        {
            get => _dbContext.Database.CurrentTransaction.GetDbTransaction(); 
            set => Transaction = value;
        }

        /// <inheritdoc />
        public IDbTransaction BeginTransaction()
        {
            return _dbContext.Database.BeginTransaction().GetDbTransaction();
        }

        /// <inheritdoc />
        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public int Commit()
        {
            return _dbContext.SaveChanges();
        }

        /// <inheritdoc />
        public void Rollback()
        {
            _dbContext.Database.RollbackTransaction();
        }
    }
}