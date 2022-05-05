using System.Data;

namespace CqrsPipeline.DataAccess
{
    /// <summary>
    /// Unit of work contract
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Connection to database
        /// </summary>
        IDbConnection Connection { get; }
        /// <summary>
        /// Current db transaction
        /// </summary>
        IDbTransaction Transaction { get; set; }
        /// <summary>
        /// Commit transaction async
        /// </summary>
        /// <returns></returns>
        Task<int> CommitAsync();
        /// <summary>
        /// Commit transaction
        /// </summary>
        /// <returns></returns>
        int Commit();
        /// <summary>
        /// Begin new transaction manually
        /// </summary>
        /// <returns></returns>
        IDbTransaction BeginTransaction();
        /// <summary>
        /// Rollback the current transaction
        /// </summary>
        void Rollback();
    }
}
