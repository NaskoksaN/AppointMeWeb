﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace AppointMeWeb.Infrastrucure.Data.Common
{
    /// <summary>
    /// Implementation of repository access methods
    /// for Relational Database Engine
    /// </summary>
    /// <typeparam name="T">Type of the data table to which 
    /// current reposity is attached</typeparam>
    public class SqlRepository : IRepository
    {
        /// <summary>
        /// Entity framework DB context holding connection information and properties
        /// and tracking entity states 
        /// </summary>
        protected DbContext Context;

        /// <summary>
        /// Representation of table in database
        /// </summary>
        protected DbSet<T> DbSet<T>() where T : class
        {
            return this.Context.Set<T>();
        }

        public SqlRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Add entity to database
        /// </summary>
        /// <param name="entity">Entity to add</param>
        public async Task AddAsync<T>(T entity) where T : class
        {
            await this.DbSet<T>().AddAsync(entity);
        }

        /// <summary>
        /// Ads collection of entities to database
        /// </summary>
        /// <typeparam name="T">Enumerable list of entities</typeparam>
        public async Task AddRangeAsync<T>(IEnumerable<T> entities) where T : class
        {
            await this.DbSet<T>().AddRangeAsync(entities);
        }

        /// <summary>
        /// All records in table
        /// </summary>
        /// <returns>Queryable expression tree</returns>
        public IQueryable<T> All<T>() where T : class
        {
            return this.DbSet<T>().AsQueryable();
        }

        public IQueryable<T> All<T>(Expression<Func<T, bool>> search) where T : class
        {
            return this.DbSet<T>().Where(search);
        }

        /// <summary>
        /// The result collection won't be tracked by the context
        /// </summary>
        /// <returns>Expression tree</returns>
        public IQueryable<T> AllReadOnly<T>() where T : class
        {
            return this.DbSet<T>()
                    .AsNoTracking();
        }

        public IQueryable<T> AllReadOnly<T>(Expression<Func<T, bool>> search) where T : class
        {
            return this.DbSet<T>()
                .Where(search)
                .AsNoTracking();
        }

        /// <summary>
        /// Deletes a record from database
        /// </summary>
        /// <param name="entity">Entity representing record to be deleted</param>
        public void Delete<T>(T entity) where T : class
        {
            EntityEntry entry = this.Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.DbSet<T>().Attach(entity);
            }
            entry.State=EntityState.Deleted;
        }

        /// <summary>
        /// Deletes a record from database
        /// </summary>
        /// <param name="id">Identificator of record to be deleted</param>
        public async Task DeleteAsync<T>(object id) where T : class
        {
            T entity = await GetByIdAsync<T>(id);
            Delete<T>(entity);
        }

        public void DeleteRange<T>(IEnumerable<T> entities) where T : class
        {
            this.DbSet<T>().RemoveRange(entities);
        }

        public void DeleteRange<T>(Expression<Func<T, bool>> deleteWhereClause) where T : class
        {
            var entities=All<T>(deleteWhereClause);
            DeleteRange(entities);
        }

        /// <summary>
        /// Detaches given entity from the context
        /// </summary>
        /// <param name="entity">Entity to be detached</param>
        public void Detach<T>(T entity) where T : class
        {
            EntityEntry entry = this.Context.Entry(entity);
            entry.State = EntityState.Detached;
        }

        /// <summary>
        /// Disposing the context when it is not neede
        /// Don't have to call this method explicitely
        /// Leave it to the IoC container
        /// </summary>
        public void Dispose()
        {
            this.Context.Dispose();
        }

        /// <summary>
        /// Gets specific record from database by primary key
        /// </summary>
        /// <param name="id">record identificator</param>
        /// <returns>Single record</returns>
        public async Task<T?> GetByIdAsync<T>(object id) where T : class
        {
            return await DbSet<T>().FindAsync(id);
        }

        public async Task<T?> GetByIdAsync<T>(object[] id) where T : class
        {
            return await DbSet<T>().FindAsync(id);
        }

        /// <summary>
        /// Saves all made changes in trasaction
        /// </summary>
        /// <returns>Error code</returns>
        public async Task<int> SaveChangesAsync()
        {
            return await this.Context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates a record in database
        /// </summary>
        /// <param name="entity">Entity for record to be updated</param>
        public void Update<T>(T entity) where T : class
        {
            this.DbSet<T>().Update(entity);
        }

        /// <summary>
        /// Updates set of records in the database
        /// </summary>
        /// <param name="entities">Enumerable collection of entities to be updated</param>
        public void UpdateRange<T>(IEnumerable<T> entities) where T : class
        {
            this.DbSet<T>().UpdateRange(entities);
        }
    }
}
