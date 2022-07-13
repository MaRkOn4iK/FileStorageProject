using DAL.Data;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FileStorageTests.Helpers
{
    internal class FakeDbContext
    {
        public static DbContextOptions<FileStorageContext> GetUnitTestDbOptions()
        {
            var options = new DbContextOptionsBuilder<FileStorageContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new FileStorageContext(options))
            {
                SeedData(context);
            }

            return options;
        }


        public static void SeedData(FileStorageContext context)
        {
            context.FileType.Add(new FileType
            {
                Id = 1,
                TypeName = "zip"
            });
            context.FileType.Add(new FileType
            {
                Id = 2,
                TypeName = "jfif"
            });
            context.FileType.Add(new FileType
            {
                Id = 3,
                TypeName = "docx"
            });


            context.File.Add(new DAL.Entities.File
            {
                Id = 1,
                FileName = "First",
                FileCreateDate = DateTime.Now,
                FileStreamCol = new byte[1],
                FileTypeId = 1,

            });
            context.File.Add(new DAL.Entities.File
            {
                Id = 2,
                FileName = "Second",
                FileCreateDate = DateTime.Now,
                FileStreamCol = new byte[1],
                FileTypeId = 2,

            });
            context.File.Add(new DAL.Entities.File
            {
                Id = 3,
                FileName = "Third",
                FileCreateDate = DateTime.Now,
                FileStreamCol = new byte[1],
                FileTypeId = 2,

            });
            context.File.Add(new DAL.Entities.File
            {
                Id = 4,
                FileName = "Fourth",
                FileCreateDate = DateTime.Now,
                FileStreamCol = new byte[1],
                FileTypeId = 3,

            });


            context.FileSecureLevel.Add(new FileSecureLevel { Id = 1, SecureLevelName = "public" });
            context.FileSecureLevel.Add(new FileSecureLevel { Id = 2, SecureLevelName = "private" });


            context.User.Add(new User { Id = 1, Name = "Mark", LastName = "Amosov", Login = "Murk", Password = "Mark123", Email = "380669978812mark@gmail.com" });
            context.User.Add(new User { Id = 2, Name = "Oksana", LastName = "Demchenko", Login = "Ksy", Password = "Oksana123", Email = "Demchenko@gmail.com" });
            context.SaveChanges();

            context.FullFileInfo.Add(new FullFileInfo
            {
                User = context.User.First(),
                File = context.File.First(),
                FileId = 1,
                FileSecureLevelId = 1,
                FileSecureLevel = context.FileSecureLevel.First(),
                UserId = 1,
                Id = 1
            });

            context.FullFileInfo.Add(new FullFileInfo
            {
                User = context.User.First(),
                File = context.File.Last(),
                FileId = 1,
                FileSecureLevelId = 2,
                FileSecureLevel = context.FileSecureLevel.Last(),
                UserId = 1,
                Id = 2
            });

            context.FullFileInfo.Add(new FullFileInfo
            {
                User = context.User.Last(),
                File = context.File.Where(r => r.Id == 3).First(),
                FileId = 3,
                FileSecureLevelId = 2,
                FileSecureLevel = context.FileSecureLevel.Last(),
                UserId = 2,
                Id = 3
            });

            context.FullFileInfo.Add(new FullFileInfo
            {
                User = context.User.Last(),
                File = context.File.Where(r => r.Id == 2).First(),
                FileId = 2,
                FileSecureLevelId = 1,
                FileSecureLevel = context.FileSecureLevel.First(),
                UserId = 2,
                Id = 4
            });

            context.SaveChanges();
        }
    }
}
