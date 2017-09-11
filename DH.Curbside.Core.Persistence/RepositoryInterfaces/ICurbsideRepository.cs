using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DH.Curbside.Core.Persistence.RepositoryInterfaces
{
    /// <summary>
    /// Base Repository Interface
    /// </summary>
    /// <typeparam name="TEntity">Generic type</typeparam>
    public interface ICurbsideRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Gets all objects
        /// </summary>
        /// <returns>Returns IQueryable object</returns>
        IQueryable<TEntity> All();

        /// <summary>
        /// Finds the object based on expression
        /// </summary>
        /// <param name="expression">expression</param>
        /// <returns>Returns object</returns>
        TEntity FindBy(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// FilterBy passed expression and returns IQueryable objects
        /// </summary>
        /// <param name="expression">expression</param>
        /// <returns>Returns IQueryable objects</returns>
        IQueryable<TEntity> FilterBy(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// Finds entity by passed Id
        /// </summary>
        /// <param name="id">Primary key Id</param>
        /// <returns>Entity object</returns>
        TEntity FindBy(int id);

        /// <summary>
        /// Adds new entity
        /// </summary>
        /// <param name="entity">entity object</param>
        /// <returns>Primary Key Id generated</returns>
        Task<int> Add(TEntity entity);

        /// <summary>
        /// Adds collection of entities
        /// </summary>
        /// <param name="items">IEnumerable objects</param>
        /// <returns>returns Boolean</returns>
        Task<bool> Add(IEnumerable<TEntity> items);

        /// <summary>
        /// Updates Entity
        /// </summary>
        /// <param name="entity">entity object</param>
        /// <returns>returns Boolean</returns>
        Task<bool> Update(TEntity entity);

        /// <summary>
        /// Updates list of Entities
        /// </summary>
        /// <param name="items">IEnumerable objects</param>
        /// <returns></returns>
        Task<bool> Update(IEnumerable<TEntity> items);

        /// <summary>
        /// Deletes passed entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>returns Boolean</returns>
        Task<bool> Delete(TEntity entity);

        /// <summary>
        /// Deletes all the passed IEnumerable entities.
        /// </summary>
        /// <param name="entities">IEnumerable objects</param>
        /// <returns>returns Boolean</returns>
        Task<bool> Delete(IEnumerable<TEntity> entities);
    }
}
