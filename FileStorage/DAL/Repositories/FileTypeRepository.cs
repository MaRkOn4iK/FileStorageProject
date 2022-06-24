using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    /// <summary>
    /// Repository for FileType table
    /// </summary>
    public class FileTypeRepository : IRepository<FileType>
    {
        private readonly FileStorageContext context;
        public FileTypeRepository(FileStorageContext context)
        {
            this.context = context;
        }

        public void Add(FileType entity)
        {
            _ = context.FileType.Add(entity);
        }

        public void DeleteById(int id)
        {
            FileType tmp = context.FileType.FirstOrDefault(c => c.Id == id);
            if (tmp != null)
            {
                _ = context.FileType.Remove(tmp);
            }
        }

        public IEnumerable<FileType> GetAll()
        {
            return context.FileType;
        }

        public async Task<FileType> GetByIdAsync(int id)
        {
            return await context.FileType.FindAsync(id);
        }

        public void Update(FileType entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            _ = context.SaveChanges();
        }

    }
}
