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
    [TestFixture]
    internal class UserServiceTests
    {
        static object[] UsersWithNullField =
        {
        new User { Login = null, Password = "qwe", Name = "qwe", Email = "qwe", LastName = "qwe" },
        new User { Login = "qwe", Password = null, Name = "qwe", Email = "mark@gmail.com", LastName = "qwe" },
        new User { Login = "", Password = null, Name = "qwe", Email = "mark@gmail.com", LastName = "qwe" },

        };
        private FakeDbSet<User> userDbSet;
        private Mock<FileStorageContext> contextMock;
        private UserRepository repository;
        Mock<IUnitOfWork> uow;
        UserService us;
        [SetUp]
        public void Reload()
        {
            userDbSet = new FakeUserDbSet();
            contextMock = new Mock<FileStorageContext>();
            contextMock.Setup(dbContext => dbContext.User).Returns(userDbSet);
            repository = new UserRepository(contextMock.Object);
            SeedData();
            uow = new Mock<IUnitOfWork>();
            uow.Setup(res => res.UserRepository).Returns(repository);
            us = new UserService(uow.Object);
        }
        private void SeedData()
        {
            repository.Add(new User { Id = 1, Login = "MaRkOn4iK", Password = "Mark123123", LastName = "Amosov", Name = "Mark", Email = "Mark@gmail.com" });
            repository.Add(new User { Id = 2, Login = "Deniska", Password = "Deniska12345", LastName = "Lipov", Name = "Denis", Email = "Denis@gmail.com" });
            repository.Add(new User { Id = 3, Login = "Kirryha", Password = "Kirryha54321", LastName = "Zolov", Name = "Kirill", Email = "Kirill@gmail.com" });
            repository.Add(new User { Id = 4, Login = "Irishka", Password = "Irka15144", LastName = "Kolova", Name = "Irina", Email = "Irina@gmail.com" });
        }

        [TestCaseSource(nameof(UsersWithNullField))]
        public async Task UserService_AddAsync_ThrowFileStorageException(User obj)
        {
            try
            {
                await us.AddAsync(obj);
                Assert.Fail();
            }
            catch (FileStorageException)
            {

            }
            catch
            {
                Assert.Fail();
            }

        }
        [Test]
        public async Task UserService_AddAsync_ReturnCorrectValue()
        {
            try
            {
                await us.AddAsync(new User { Login = "qwe", Password = "qwe", Name = "qwe", Email = "qwe", LastName = "qwe" });
                Assert.That(us.GetAll().Count,Is.EqualTo(5));
            }
            catch
            {
                Assert.Fail();
            }

        }
        [Test]
        public async Task UserService_ChangeAll_ReturnCorrectValue()
        {
            try
            {
                await us.ChangeAll("MaRkOn4iK","Markkkk", "Mark123123", "Amosov", "Mark", "Mark@gmail.com");
                var tmp = await us.GetByIdAsync(1);
                Assert.That(tmp.Login, Is.EqualTo("Markkkk"));
            }
            catch
            {
                Assert.Fail();
            }

        }

        [TestCaseSource(nameof(UsersWithNullField))]
        public async Task UserService_ChangeAll_ThrowFileStorageException(User obj)
        {
            try
            {
                await us.ChangeAll("MaRkOn4iK", obj.Login, obj.Password,obj.Name, obj.LastName, obj.Email);
                Assert.Fail();
            }
            catch(FileStorageException)
            {

            }
            catch
            {
                Assert.Fail();
            }

        }
        [Test]
        public async Task UserService_DeleteById_ReturnCorrectValue()
        {
            try
            {
                await us.DeleteAsync(1);
                Assert.That(us.GetAll().Count, Is.EqualTo(3));
            }
            catch
            {
                Assert.Fail();
            }

        }
        [Test]
        public async Task UserService_DeleteAsyncByLogin_ReturnCorrectValue()
        {
            try
            {
                var FileDbSet = new FakeFullFileInfoDbSet();
                contextMock.Setup(dbContext => dbContext.FullFileInfo).Returns(FileDbSet);
                var Filerepository = new FullFileInfoRepository(contextMock.Object);
                uow = new Mock<IUnitOfWork>();
                uow.Setup(res => res.FullFileInfoRepository).Returns(Filerepository);
                uow.Setup(res => res.UserRepository).Returns(repository);
                us = new UserService(uow.Object);
                await us.DeleteAsyncByLogin("Deniska");
                Assert.That(us.GetAll().Count, Is.EqualTo(3));
            }
            catch
            {
                Assert.Fail();
            }

        }

    }
}
