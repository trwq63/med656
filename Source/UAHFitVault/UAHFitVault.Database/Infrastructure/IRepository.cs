using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UAHFitVault.Database.Infrastructure
{
    /// <summary>
    /// Interface for the generic repository pattern
    /// </summary>
    /// <typeparam name="T">Indicates generic</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Marks an entity as new
        /// </summary>
        /// <param name="entity">The entity to add to the database of type T</param>
        void Add(T entity);

        /// <summary>
        /// Marks an entity as modified
        /// </summary>
        /// <param name="entity">The entity to update to the database of type T</param>
        void Update(T entity);

        /// <summary>
        /// Marks an entity to be removed
        /// </summary>
        /// <param name="entity">The entity to remove to the database of type T</param>
        void Delete(T entity);

        /// <summary>
        /// Delete an entity using a delegate
        /// </summary>
        /// <param name="where">The delegate that will be used for the delete query</param>
        void Delete(Expression<Func<T, bool>> where);

        /// <summary>
        /// Get an entity by int id
        /// </summary>
        /// <param name="id">Id used for the get request to the database</param>
        /// <returns></returns>
        T GetById(int id);

        /// <summary>
        /// Get an entity using delegate
        /// </summary>
        /// <param name="where">Delegate used to get the entity</param>
        /// <returns></returns>
        T Get(Expression<Func<T, bool>> where);

        /// <summary>
        /// Gets all entities of type T
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Gets entities using delegate
        /// </summary>
        /// <param name="where">Delegate used to get the entities</param>
        /// <param name="order">Delegate used to order results</param>
        /// <param name="skip">Skip a number of records in the data collection</param>
        /// <param name="take">Number of records to return.</param>
        /// <returns></returns>
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where, Expression<Func<T, object>> order = null, int skip = 0, int take = 0);

        /// <summary>
        /// Gets entities using delegate
        /// </summary>
        /// <param name="where">Delegate used to get the entities</param>
        /// <param name="order">DateTime object used to order results</param>
        /// <param name="skip">Skip a number of records in the data collection</param>
        /// <param name="take">Number of records to return.</param>
        /// <returns></returns>
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where, Expression<Func<T, DateTime>> order = null, int skip = 0, int take = 0);
    }
}
