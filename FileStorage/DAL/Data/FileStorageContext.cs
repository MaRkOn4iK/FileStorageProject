using DAL.Entities;
using System.Data.Entity;

namespace DAL.Data
{
    /// <summary>
    /// DbContext class with connection string
    /// </summary>
    public partial class FileStorageContext : DbContext
    {
        public FileStorageContext()
            : base(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = C:\USERS\38066\ONEDRIVE\–¿¡Œ◊»… —“ŒÀ\EPAMPROJECT\FILESTORAGE.MDF; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False")
        {

        }
        public virtual DbSet<File> File { get; set; }
        public virtual DbSet<FileSecureLevel> FileSecureLevel { get; set; }
        public virtual DbSet<FileType> FileType { get; set; }
        public virtual DbSet<FullFileInfo> FullFileInfo { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
