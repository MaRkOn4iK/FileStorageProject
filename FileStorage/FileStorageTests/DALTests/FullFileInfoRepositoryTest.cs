using DAL.Data;
using DAL.Entities;
using DAL.Repositories;
using FileStorageTests.Helpers;
using Moq;

namespace FileStorageTests.DALTests
{
    internal class FullFileInfoRepositoryTest
    {
        [Test]
        public void FullFileInfoRepository_GetAllAsync_ReturnsAllValues()
        {
            using var context = new FileStorageContext(FakeDbContext.GetUnitTestDbOptions());
            FullFileInfoRepository repository = new FullFileInfoRepository(context);
            var user = repository.GetAll();
            Assert.That(user.Count, Is.EqualTo(4));
        }
        [Test]
        public void FullFileInfoRepository_DeleteById_ReturnsCorrectValues()
        {
            using var context = new FileStorageContext(FakeDbContext.GetUnitTestDbOptions());
            FullFileInfoRepository repository = new FullFileInfoRepository(context);
            repository.DeleteById(2);
            context.SaveChanges();  
            var user = repository.GetAll();
            Assert.That(user.Count, Is.EqualTo(3));
        }
        [Test]
        public async Task FullFileInfoRepository_GetById_ReturnsCorrectValues()
        {
            using var context = new FileStorageContext(FakeDbContext.GetUnitTestDbOptions());
            FullFileInfoRepository repository = new FullFileInfoRepository(context);
            var file = await repository.GetByIdAsync(1);
            Assert.That(file.Id, Is.EqualTo(1));
            Assert.That(file.File.FileName, Is.EqualTo("First"));
        }
        [Test]
        public void FullFileInfoRepository_AddNewFile_ReturnsCorrectValues()
        {
            using var context = new FileStorageContext(FakeDbContext.GetUnitTestDbOptions());
            FullFileInfoRepository repository = new FullFileInfoRepository(context);
            repository.Add(new FullFileInfo
            {
                Id = 5,
                User = context.User.First(),
                UserId = 1,
                FileSecureLevel = context.FileSecureLevel.First(),
                FileSecureLevelId = 1,
                FileId = 1,
                File = context.File.First()
            });
            context.SaveChanges();
            var user = repository.GetAll();
            Assert.That(user.Count, Is.EqualTo(5));
        }
        [Test]
        public async Task FullFileInfoRepository_Update_ReturnsCorrectValues()
        {
            using var context = new FileStorageContext(FakeDbContext.GetUnitTestDbOptions());
            FullFileInfoRepository repository = new FullFileInfoRepository(context);
            var file = await repository.GetByIdAsync(1);
            file.File.FileName = "NewFirst";
            repository.Update(file);
            var file2 = await repository.GetByIdAsync(1);
            Assert.That(file2.File.FileName, Is.EqualTo("NewFirst"));
            Assert.That(file2.Id, Is.EqualTo(1));
        }
    }
}
