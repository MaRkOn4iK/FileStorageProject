using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    /// <summary>
    /// Interface for repositories with CRUD operations
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();

        Task<TEntity> GetByIdAsync(int id);

        void Add(TEntity entity);


        void DeleteById(int id);

        void Update(TEntity entity);
    }
}
