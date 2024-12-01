using System.Linq.Expressions;

namespace AppointMeWeb.Infrastrucure.Data.Common
{
    /// <summary>
    /// Abstraction of repository access methods
    /// </summary>
    public interface IRepository 
    {
        /// <summary>
        /// All records from table
        /// </summary>
        /// <returns>Queryable expression tree</returns>
        IQueryable<T> All<T>() where T:class;

        /// <summary>
        /// All records from table
        /// </summary>
        /// <returns>Queryable expression tree</returns>
        IQueryable<T> All<T> (Expression<Func<T, bool>> search) where T : class;

        /// <summary>
        /// Return No tracking collection
        /// </summary>
        /// <returns>Expression tree</returns>
        IQueryable<T> AllReadOnly<T>() where T : class;

        /// <summary>
        /// Return No tracking collection
        /// </summary>
        /// <returns>Expression tree</returns>
        IQueryable<T> AllReadOnly<T>(Expression<Func<T,bool>> search) where T : class;

        /// <summary>
        /// Get specific record from DB by primary key
        /// </summary>
        /// <param name="id">record identificato</param>
        /// <returns>Single record</returns>
        Task<T> GetByIdAsync<T>(object id) where T : class;
    
        Task<T> GetByIdAsync<T>(object[] id) where T : class;

        /// <summary>
        /// Add entity to database
        /// </summary>
        /// <returns>Entity to add</returns>
        Task AddAsync<T> (T entity) where T : class;

        /// <summary>
        /// Add collection of entities to database
        /// </summary>
        /// <returns>Enumerable list of entities</returns>
        Task AddRangeAsync<T> (IEnumerable<T> entities) where T : class;

        /// <summary>
        /// Update record in database
        /// </summary>
        /// <param name="entity">Entity for record to be updated</param>
        void Update<T> (T entity) where T : class;

        /// <summary>
        /// Update set of records in database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities">Enumerable collection of entities to be updated</param>
        void UpdateRange<T> (IEnumerable<T> entities)where T : class;

        /// <summary>
        /// Delete record with given Id/primare key from database
        /// </summary>
        /// <param name="id">Identificator of record to be deleted</param>
        Task DeleteAsync<T> (object id) where T : class;

        /// <summary>
        /// Deletes a record from database
        /// </summary>
        /// <param name="entity">Entity representing record to be deleted</param>
        void Delete<T>(T entity) where T : class;

        void DeleteRange<T> (IEnumerable<T> entities) where T : class;
        void DeleteRange<T> (Expression<Func<T, bool>> deleteWhereClause) where T : class;

        /// <summary>
        /// Detach given entity from the context
        /// </summary>
        /// <param name="entity">Entity to be detached</param>
        void Detach<T>(T entity) where T : class;

        /// <summary>
        /// Saves all made changes in transaction
        /// </summary>
        /// <returns>Erro code</returns>
        Task<int> SaveChangesAsync();

        IQueryable<T> Include<T>(IQueryable<T> query, Expression<Func<T, object>> includeExpression) where T : class;
       
    }
}
