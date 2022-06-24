using DAL.Data;
using DAL.Entities;
using DAL.Repositories;
using FileStorageTests.Helpers;
using Moq;

namespace FileStorageTests.DALTests
{
    internal class FileRepositoryTests
    {
        private FakeDbSet<DAL.Entities.File> fileDbSet;
        private Mock<FileStorageContext> contextMock;
        private FileRepository repository;
        [SetUp]
        public void Reload()
        {
            fileDbSet = new FakeFileDbSet();
            contextMock = new Mock<FileStorageContext>();
            contextMock.Setup(dbContext => dbContext.File).Returns(fileDbSet);
            repository = new FileRepository(contextMock.Object);
            SeedData();
        }

        private void SeedData()
        {
            repository.Add(new DAL.Entities.File
            {
                Id = 1,
                FileName = "First",
                FileCreateDate = DateTime.Now,
                FileStreamCol = new byte[1],
                FileTypeId = 1,
                FileType = new FileType { Id = 1, TypeName = "pdf" }
            });
            repository.Add(new DAL.Entities.File
            {
                Id = 2,
                FileName = "Second",
                FileCreateDate = DateTime.Now,
                FileStreamCol = new byte[1],
                FileTypeId = 1,
                FileType = new FileType { Id = 1, TypeName = "pdf" }
            });
            repository.Add(new DAL.Entities.File
            {
                Id = 3,
                FileName = "Third",
                FileCreateDate = DateTime.Now,
                FileStreamCol = new byte[1],
                FileTypeId = 1,
                FileType = new FileType { Id = 1, TypeName = "pdf" }
            });
            repository.Add(new DAL.Entities.File
            {
                Id = 4,
                FileName = "Fourth",
                FileCreateDate = DateTime.Now,
                FileStreamCol = new byte[1],
                FileTypeId = 1,
                FileType = new FileType { Id = 1, TypeName = "pdf" }
            });

        }

        [Test]
        public void FullFileInfoRepository_GetAllAsync_ReturnsAllValues()
        {

            var user = repository.GetAll();
            Assert.That(user.Count, Is.EqualTo(4));
        }
        [Test]
        public void FullFileInfoRepository_DeleteById_ReturnsCorrectValues()
        {
            repository.DeleteById(2);
            var user = repository.GetAll();
            Assert.That(user.Count, Is.EqualTo(3));
        }
        [Test]
        public async Task FullFileInfoRepository_GetById_ReturnsCorrectValues()
        {

            var file = await repository.GetByIdAsync(1);
            Assert.That(file.Id, Is.EqualTo(1));
            Assert.That(file.FileName, Is.EqualTo("First"));
        }
        [Test]
        public void FullFileInfoRepository_AddNewFile_ReturnsCorrectValues()
        {
            repository.Add(new DAL.Entities.File
            {
                Id = 5,
                FileName = "Fifth",
                FileCreateDate = DateTime.Now,
                FileStreamCol = new byte[1],
                FileTypeId = 1,
                FileType = new FileType { Id = 1, TypeName = "pdf" }
            });
            var user = repository.GetAll();
            Assert.That(user.Count, Is.EqualTo(5));
        }
        [Test]
        public async Task FullFileInfoRepository_Update_ReturnsCorrectValues()
        {

            var file = await repository.GetByIdAsync(1);
            file.FileName = "NewFirst";
            repository.Update(file);
            var file2 = await repository.GetByIdAsync(1);
            Assert.That(file2.FileName, Is.EqualTo("NewFirst"));
            Assert.That(file2.Id, Is.EqualTo(1));
        }
    }
}
