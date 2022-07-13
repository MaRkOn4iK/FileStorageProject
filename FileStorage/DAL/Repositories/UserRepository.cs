using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    /// <summary>
    ///  Repository for User table
    /// </summary>
    public class UserRepository : IRepository<User>
    {
        private FileStorageContext context;
        public UserRepository(FileStorageContext context)
        {
            this.context = context;
        }

        public void Add(User entity)
        {
            this.context.User.Add(entity);
        }

        public void DeleteById(int id)
        {
            var tmp = this.context.User.FirstOrDefault(c => c.Id == id);
            if (tmp != null)
            {
                this.context.User.Remove(tmp);
            }
        }

        public IEnumerable<User> GetAll()
        {
            foreach (var item in context.User)
            {
                System.Console.WriteLine(item);
            }
            return context.User;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await context.User.FindAsync(id);
        }

        public void Update(User entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
