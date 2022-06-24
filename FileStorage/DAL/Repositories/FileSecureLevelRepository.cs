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
    /// Repository for FileSecureLevel table
    /// </summary>
    public class FileSecureLevelRepository : IRepository<FileSecureLevel>
    {
        private readonly FileStorageContext context;
        public FileSecureLevelRepository(FileStorageContext context)
        {
            this.context = context;
        }

        public void Add(FileSecureLevel entity)
        {
            _ = context.FileSecureLevel.Add(entity);
        }

        public void DeleteById(int id)
        {
            FileSecureLevel tmp = context.FileSecureLevel.FirstOrDefault(c => c.Id == id);
            if (tmp != null)
            {
                _ = context.FileSecureLevel.Remove(tmp);
            }
        }

        public IEnumerable<FileSecureLevel> GetAll()
        {
            return context.FileSecureLevel;
        }

        public async Task<FileSecureLevel> GetByIdAsync(int id)
        {
            return await context.FileSecureLevel.FindAsync(id);
        }

        public void Update(FileSecureLevel entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            _ = context.SaveChanges();
        }

    }
}
