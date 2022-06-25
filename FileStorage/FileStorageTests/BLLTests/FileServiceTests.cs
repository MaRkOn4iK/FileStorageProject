using BLL.Services;
using BLL.Validation;
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
        private FakeDbSet<FullFileInfo> fileDbSet;
        private Mock<FileStorageContext> contextMock;
        private FullFileInfoRepository repository;
        Mock<IUnitOfWork> uow;
        FileService fs;

        [SetUp]
        public void Reload()
        {
            fileDbSet = new FakeFullFileInfoDbSet();
            contextMock = new Mock<FileStorageContext>();
            contextMock.Setup(dbContext => dbContext.FullFileInfo).Returns(fileDbSet);
            repository = new FullFileInfoRepository(contextMock.Object);
            SeedData();
            uow = new Mock<IUnitOfWork>();
            uow.Setup(res => res.FullFileInfoRepository).Returns(repository);
            fs = new FileService(uow.Object);
        }
        private void SeedData()
        {
            repository.Add(new FullFileInfo
            {
                Id = 1,
                User = new User { Id = 1, Login = "MaRkOn4iK", Password = "Mark123123", LastName = "Amosov", Name = "Mark", Email = "Mark@gmail.com" },
                UserId = 1,
                FileSecureLevel = new FileSecureLevel { Id = 1, SecureLevelName = "public" },
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
        public async Task FileService_AddAsync_ReturnsCorrectValue()
        {
            await fs.AddAsync(new FullFileInfo
            {
                Id = 4,
                User = new User { Id = 1, Login = "MaRkOn4iK", Password = "Mark123123", LastName = "Amosov", Name = "Mark", Email = "Mark@gmail.com" },
                UserId = 1,
                FileSecureLevel = new FileSecureLevel { Id = 1, SecureLevelName = "public" },
                FileSecureLevelId = 1,
                FileId = 4,
                File = new DAL.Entities.File { Id = 3, FileName = "Fourth", FileCreateDate = DateTime.Now, FileStreamCol = new byte[1], FileTypeId = 1, FileType = new FileType { Id = 1, TypeName = "pdf" } }

            });
            Assert.That(uow.Object.FullFileInfoRepository.GetAll().Count, Is.EqualTo(4));
        }

        [TestCase(1, 2)]
        [TestCase(2, 2)]
        [TestCase(10, 3)]
        [TestCase(12, 3)]
        public async Task FileService_DeleteAsync_RetutnsCorrectValue(int index, int expected)
        {
            await fs.DeleteAsync(index);
            Assert.That(uow.Object.FullFileInfoRepository.GetAll().Count, Is.EqualTo(expected));
        }

        [Test]
        public void FileService_GetAllByUser_ReturnsCorrectValue()
        {
            var list = fs.GetAllByUser(repository.GetByIdAsync(1).Result.User);
            Assert.That(list.Count, Is.EqualTo(3));
        }

        [Test]
        public void FileService_GetAllPrivateByUser_ReturnsCorrectValue()
        {
            var list = fs.GetAllPrivateByUser(repository.GetByIdAsync(1).Result.User);
            Assert.That(list.Count, Is.EqualTo(2));
        }

        [Test]
        public void FileService_GetAllPublicFiles_ReturnsCorrectValue()
        {
            var list = fs.GetAllPublicFiles();
            Assert.That(list.Count, Is.EqualTo(1));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task FileService_GetByIdAsync_ReturnsCorrectValue(int index)
        {
            var file = await fs.GetByIdAsync(index);
            switch (index)
            {
                case 1: Assert.That(file.File.FileName, Is.EqualTo("First")); break;
                case 2: Assert.That(file.File.FileName, Is.EqualTo("Second")); break;
                case 3: Assert.That(file.File.FileName, Is.EqualTo("Third")); break;
                default:
                    Assert.Fail();
                    break;
            }
        }

        [TestCase(1, "First")]
        [TestCase(2, "Second")]
        [TestCase(3, "Third")]
        [TestCase(4, "Fourth")]
        public void FileService_GetByName_ReturnsCorrectValue(int caseValue, string fileName)
        {
            try
            {
                var file = fs.GetByName(fileName, new User { Id = 1, Login = "MaRkOn4iK", Password = "Mark123123", LastName = "Amosov", Name = "Mark", Email = "Mark@gmail.com" });

                switch (caseValue)
                {
                    case 1: Assert.That(file.File.FileName, Is.EqualTo("First")); break;
                    case 2: Assert.That(file.File.FileName, Is.EqualTo("Second")); break;
                    case 3: Assert.That(file.File.FileName, Is.EqualTo("Third")); break;
                    case 4: Assert.Fail(); break;

                    default: break;
                }
            }
            catch(FileStorageException)
            {
                Assert.That(caseValue, Is.EqualTo(4));
            }
            catch (Exception)
            {
                Assert.Fail();
            }
           
        }
    }
}
