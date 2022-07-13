using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    /// <summary>
    /// DbContext class with connection string
    /// </summary>
    public partial class FileStorageContext : DbContext
    {
        public FileStorageContext(DbContextOptions<FileStorageContext> options) : base(options)
        {

        }
        public virtual DbSet<Entities.File> File { get; set; }
        public virtual DbSet<FileSecureLevel> FileSecureLevel { get; set; }
        public virtual DbSet<FileType> FileType { get; set; }
        public virtual DbSet<FullFileInfo> FullFileInfo { get; set; }
        public virtual DbSet<User> User { get; set; }
    }
}
