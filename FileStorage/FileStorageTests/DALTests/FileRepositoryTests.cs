using DAL.Data;
using DAL.Repositories;
using FileStorageTests.Helpers;

namespace FileStorageTests.DALTests
{
    internal class FileRepositoryTests
    {

        [Test]
        public void FullFileInfoRepository_GetAllAsync_ReturnsAllValues()
        {
            using var context = new FileStorageContext(FakeDbContext.GetUnitTestDbOptions());
            FileRepository repository1 = new FileRepository(context);
            var user = repository1.GetAll();
            Assert.That(user.Count, Is.EqualTo(4));
        }
        [Test]
        public void FullFileInfoRepository_DeleteById_ReturnsCorrectValues()
        {
            using var context = new FileStorageContext(FakeDbContext.GetUnitTestDbOptions());
            FileRepository repository = new FileRepository(context);
            repository.DeleteById(2);
            context.SaveChanges();
            var user = repository.GetAll();
            Assert.That(user.Count, Is.EqualTo(3));
        }
        [Test]
        public async Task FullFileInfoRepository_GetById_ReturnsCorrectValues()
        {
            using var context = new FileStorageContext(FakeDbContext.GetUnitTestDbOptions());
            FileRepository repository = new FileRepository(context);
            var file = await repository.GetByIdAsync(1);
            Assert.That(file.Id, Is.EqualTo(1));
            Assert.That(file.FileName, Is.EqualTo("First"));
        }
        [Test]
        public void FullFileInfoRepository_AddNewFile_ReturnsCorrectValues()
        {
            using var context = new FileStorageContext(FakeDbContext.GetUnitTestDbOptions());
            FileRepository repository = new FileRepository(context);
            repository.Add(new DAL.Entities.File
            {
                Id = 5,
                FileName = "Fifth",
                FileCreateDate = DateTime.Now,
                FileStreamCol = new byte[1],
                FileTypeId = 1,
                FileType = context.FileType.First()
            });
            context.SaveChanges();
            var user = repository.GetAll();
            Assert.That(user.Count, Is.EqualTo(5));
        }
        [Test]
        public async Task FullFileInfoRepository_Update_ReturnsCorrectValues()
        {
            using var context = new FileStorageContext(FakeDbContext.GetUnitTestDbOptions());
            FileRepository repository = new FileRepository(context);

            var file = await repository.GetByIdAsync(1);
            file.FileName = "NewFirst";
            repository.Update(file);
            var file2 = await repository.GetByIdAsync(1);
            Assert.That(file2.FileName, Is.EqualTo("NewFirst"));
            Assert.That(file2.Id, Is.EqualTo(1));
        }
    }
}
