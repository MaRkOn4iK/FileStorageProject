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
    /// Repository for File table
    /// </summary>
    public class FileRepository : IRepository<File>
    {
        private readonly FileStorageContext context;
        public FileRepository(FileStorageContext context)
        {
            this.context = context;
        }

        public void Add(File entity)
        {
            _ = context.File.Add(entity);
        }

        public void DeleteById(int id)
        {
            File tmp = context.File.FirstOrDefault(c => c.Id == id);
            if (tmp != null)
            {
                _ = context.File.Remove(tmp);
            }
        }

        public IEnumerable<File> GetAll()
        {
            return context.File.Include(x => x.FileType);
        }

        public async Task<File> GetByIdAsync(int id)
        {
            return await context.File.FindAsync(id);
        }

        public void Update(File entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            _ = context.SaveChanges();
        }

    }
}
