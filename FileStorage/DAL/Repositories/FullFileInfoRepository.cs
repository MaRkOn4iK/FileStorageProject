using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    /// <summary>
    /// Repository for FullFileInfo table
    /// </summary>
    public class FullFileInfoRepository : IRepository<FullFileInfo>
    {
        private readonly FileStorageContext context;
        public FullFileInfoRepository(FileStorageContext context)
        {
            this.context = context;
        }

        public void Add(FullFileInfo entity)
        {
            _ = context.FullFileInfo.Add(entity);
        }

        public void DeleteById(int id)
        {
            FullFileInfo tmp = context.FullFileInfo.FirstOrDefault(c => c.Id == id);
            if (tmp != null)
            {
                _ = context.FullFileInfo.Remove(tmp);
            }
        }

        public IEnumerable<FullFileInfo> GetAll()
        {
            var tmp = context.FullFileInfo.Include(x => x.User).Include(x => x.File).Include(x => x.FileSecureLevel).Include(x=>x.File.FileType);
            foreach (var item in tmp)
            {
                Console.WriteLine(item);
            }
            return tmp;
        }

        public async Task<FullFileInfo> GetByIdAsync(int id)
        {
            return context.FullFileInfo.Where(r=>r.Id==id).Include(x => x.User).Include(x => x.File).Include(x => x.FileSecureLevel).Include(x => x.File.FileType).First();
        }

        public void Update(FullFileInfo entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            _ = context.SaveChanges();
        }

    }
}
