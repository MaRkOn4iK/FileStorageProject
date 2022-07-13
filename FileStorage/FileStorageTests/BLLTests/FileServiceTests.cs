using BLL.Services;
using BLL.Validation.Exceptions;
using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Repositories;
using FileStorageTests.Helpers;
using Moq;

namespace FileStorageTests.BLLTests
{
    internal class FileServiceTests
    {
        [Test]
        public async Task FileService_AddAsync_ReturnsCorrectValue()
        {
            using var context = new FileStorageContext(FakeDbContext.GetUnitTestDbOptions());
            FullFileInfoRepository repository = new FullFileInfoRepository(context);
            var uow = new Mock<IUnitOfWork>();
            uow.Setup(res => res.FullFileInfoRepository).Returns(repository);
            var fs = new FileService(uow.Object);
            await fs.AddAsync(new FullFileInfo
            {
                Id = 5,
                User = context.User.First(),
                UserId = 1,
                FileSecureLevel = context.FileSecureLevel.First(),
                FileSecureLevelId = 1,
                FileId = 1,
                File = context.File.First(),

            });
            context.SaveChanges();
            Assert.That(uow.Object.FullFileInfoRepository.GetAll().Count, Is.EqualTo(5));
        }

        [TestCase(1, 3)]
        [TestCase(2, 3)]
        [TestCase(10, 4)]
        [TestCase(12, 4)]
        public async Task FileService_DeleteAsync_RetutnsCorrectValue(int index, int expected)
        {
            using var context = new FileStorageContext(FakeDbContext.GetUnitTestDbOptions());
            FullFileInfoRepository repository = new FullFileInfoRepository(context);
            var uow = new Mock<IUnitOfWork>();
            uow.Setup(res => res.FullFileInfoRepository).Returns(repository);
            var fs = new FileService(uow.Object);
            await fs.DeleteAsync(index);
            context.SaveChanges();
            Assert.That(uow.Object.FullFileInfoRepository.GetAll().Count, Is.EqualTo(expected));
        }

        [Test]
        public void FileService_GetAllByUser_ReturnsCorrectValue()
        {
            using var context = new FileStorageContext(FakeDbContext.GetUnitTestDbOptions());
            FullFileInfoRepository repository = new FullFileInfoRepository(context);
            var uow = new Mock<IUnitOfWork>();
            uow.Setup(res => res.FullFileInfoRepository).Returns(repository);
            var fs = new FileService(uow.Object);
            var list = fs.GetAllByUser(repository.GetByIdAsync(1).Result.User);
            Assert.That(list.Count, Is.EqualTo(2));
        }

        [Test]
        public void FileService_GetAllPrivateByUser_ReturnsCorrectValue()
        {
            using var context = new FileStorageContext(FakeDbContext.GetUnitTestDbOptions());
            FullFileInfoRepository repository = new FullFileInfoRepository(context);
            var uow = new Mock<IUnitOfWork>();
            uow.Setup(res => res.FullFileInfoRepository).Returns(repository);
            var fs = new FileService(uow.Object);
            var list = fs.GetAllPrivateByUser(repository.GetByIdAsync(1).Result.User);
            Assert.That(list.Count, Is.EqualTo(1));
        }

        [Test]
        public void FileService_GetAllPublicFiles_ReturnsCorrectValue()
        {
            using var context = new FileStorageContext(FakeDbContext.GetUnitTestDbOptions());
            FullFileInfoRepository repository = new FullFileInfoRepository(context);
            var uow = new Mock<IUnitOfWork>();
            uow.Setup(res => res.FullFileInfoRepository).Returns(repository);
            var fs = new FileService(uow.Object);
            var list = fs.GetAllPublicFiles();
            Assert.That(list.Count, Is.EqualTo(2));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task FileService_GetByIdAsync_ReturnsCorrectValue(int index)
        {
            using var context = new FileStorageContext(FakeDbContext.GetUnitTestDbOptions());
            FullFileInfoRepository repository = new FullFileInfoRepository(context);
            var uow = new Mock<IUnitOfWork>();
            uow.Setup(res => res.FullFileInfoRepository).Returns(repository);
            var fs = new FileService(uow.Object);
            var file = await fs.GetByIdAsync(index);
            switch (index)
            {
                case 1: Assert.That(file.File.FileName, Is.EqualTo("First")); break;
                case 2: Assert.That(file.File.FileName, Is.EqualTo("Fourth")); break;
                case 3: Assert.That(file.File.FileName, Is.EqualTo("Third")); break;
                default:
                    Assert.Fail();
                    break;
            }
        }

        [TestCase(1, "First")]
        [TestCase(2, "Fourth")]
        [TestCase(5, "Fifth")]
        public void FileService_GetByName_ReturnsCorrectValue(int caseValue, string fileName)
        {
            try
            {
                using var context = new FileStorageContext(FakeDbContext.GetUnitTestDbOptions());
                FullFileInfoRepository repository = new FullFileInfoRepository(context);
                var uow = new Mock<IUnitOfWork>();
                uow.Setup(res => res.FullFileInfoRepository).Returns(repository);
                var fs = new FileService(uow.Object);
                var file = fs.GetByName(fileName, context.User.First()); 

                switch (caseValue)
                {
                    case 1: Assert.That(file.File.FileName, Is.EqualTo("First")); break;
                    case 2: Assert.That(file.File.FileName, Is.EqualTo("Fourth")); break;
                    case 5: Assert.Fail(); break;

                    default: break;
                }
            }
            catch (FileStorageException)
            {
                Assert.That(caseValue, Is.EqualTo(5));
            }
            catch (Exception)
            {
                Assert.Fail();
            }

        }
    }
}
