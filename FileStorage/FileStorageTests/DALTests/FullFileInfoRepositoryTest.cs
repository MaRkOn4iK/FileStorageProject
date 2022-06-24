using DAL.Data;
using DAL.Entities;
using DAL.Repositories;
using FileStorageTests.Helpers;
using Moq;

namespace FileStorageTests.DALTests
{
    internal class FullFileInfoRepositoryTest
    {
        private FakeDbSet<FullFileInfo> fileDbSet;
        private Mock<FileStorageContext> contextMock;
        private FullFileInfoRepository repository;
        [SetUp]
        public void Reload()
        {
            fileDbSet = new FakeFullFileInfoDbSet();
            contextMock = new Mock<FileStorageContext>();
            contextMock.Setup(dbContext => dbContext.FullFileInfo).Returns(fileDbSet);
            repository = new FullFileInfoRepository(contextMock.Object);
            SeedData();
        }

        private void SeedData()
        {
            repository.Add(new FullFileInfo
            {
                Id = 1,
                User = new User { Id = 1, Login = "MaRkOn4iK", Password = "Mark123123", LastName = "Amosov", Name = "Mark", Email = "Mark@gmail.com" },
                UserId = 1,
                FileSecureLevel = new FileSecureLevel { Id = 1, SecureLevelName = "private" },
                FileSecureLevelId = 1,
                FileId = 1,
                File = new DAL.Entities.File { Id = 1, FileName = "First", FileCreateDate = DateTime.Now, FileStreamCol = new byte[1], FileTypeId = 1, FileType = new FileType { Id = 1, TypeName = "pdf" } }
            });
            repository.Add(new FullFileInfo
            {
                Id = 2,
                User = new User { Id = 1, Login = "MaRkOn4iK", Password = "Mark123123", LastName = "Amosov", Name = "Mark", Email = "Mark@gmail.com" },
                UserId = 1,
                FileSecureLevel = new FileSecureLevel { Id = 2, SecureLevelName = "private" },
                FileSecureLevelId = 2,
                FileId = 2,
                File = new DAL.Entities.File { Id = 2, FileName = "Second", FileCreateDate = DateTime.Now, FileStreamCol = new byte[1], FileTypeId = 1, FileType = new FileType { Id = 1, TypeName = "pdf" } }
            });
            repository.Add(new FullFileInfo
            {
                Id = 3,
                User = new User { Id = 1, Login = "MaRkOn4iK", Password = "Mark123123", LastName = "Amosov", Name = "Mark", Email = "Mark@gmail.com" },
                UserId = 1,
                FileSecureLevel = new FileSecureLevel { Id = 2, SecureLevelName = "private" },
                FileSecureLevelId = 2,
                FileId = 3,
                File = new DAL.Entities.File { Id = 3, FileName = "Third", FileCreateDate = DateTime.Now, FileStreamCol = new byte[1], FileTypeId = 1, FileType = new FileType { Id = 1, TypeName = "pdf" } }
            });
        }

        [Test]
        public void FullFileInfoRepository_GetAllAsync_ReturnsAllValues()
        {

            var user = repository.GetAll();
            Assert.That(user.Count, Is.EqualTo(3));
        }
        [Test]
        public void FullFileInfoRepository_DeleteById_ReturnsCorrectValues()
        {
            repository.DeleteById(2);
            var user = repository.GetAll();
            Assert.That(user.Count, Is.EqualTo(2));
        }
        [Test]
        public async Task FullFileInfoRepository_GetById_ReturnsCorrectValues()
        {

            var file = await repository.GetByIdAsync(1);
            Assert.That(file.Id, Is.EqualTo(1));
            Assert.That(file.File.FileName, Is.EqualTo("First"));
        }
        [Test]
        public void FullFileInfoRepository_AddNewFile_ReturnsCorrectValues()
        {
            repository.Add(new FullFileInfo
            {
                Id = 4,
                User = new User { Id = 1, Login = "MaRkOn4iK", Password = "Mark123123", LastName = "Amosov", Name = "Mark", Email = "Mark@gmail.com" },
                UserId = 1,
                FileSecureLevel = new FileSecureLevel { Id = 1, SecureLevelName = "private" },
                FileSecureLevelId = 1,
                FileId = 4,
                File = new DAL.Entities.File { Id = 4, FileName = "First", FileCreateDate = DateTime.Now, FileStreamCol = new byte[1], FileTypeId = 1, FileType = new FileType { Id = 1, TypeName = "pdf" } }
            });
            var user = repository.GetAll();
            Assert.That(user.Count, Is.EqualTo(4));
        }
        [Test]
        public async Task FullFileInfoRepository_Update_ReturnsCorrectValues()
        {

            var file = await repository.GetByIdAsync(1);
            file.File.FileName = "NewFirst";
            repository.Update(file);
            var file2 = await repository.GetByIdAsync(1);
            Assert.That(file2.File.FileName, Is.EqualTo("NewFirst"));
            Assert.That(file2.Id, Is.EqualTo(1));
        }
    }
}
