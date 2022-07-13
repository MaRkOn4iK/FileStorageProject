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
    [TestFixture]
    internal class UserServiceTests
    {
        static object[] UsersWithNullField =
        {
        new User { Login = null, Password = "qwe", Name = "qwe", Email = "qwe", LastName = "qwe" },
        new User { Login = "qwe", Password = null, Name = "qwe", Email = "mark@gmail.com", LastName = "qwe" },
        new User { Login = "", Password = null, Name = "qwe", Email = "mark@gmail.com", LastName = "qwe" },

        };
        
        [TestCaseSource(nameof(UsersWithNullField))]
        public async Task UserService_AddAsync_ThrowFileStorageException(User obj)
        {
            try
            {
            using var context = new FileStorageContext(FakeDbContext.GetUnitTestDbOptions());
            UserRepository repository = new UserRepository(context);
            var uow = new Mock<IUnitOfWork>();
            uow.Setup(res => res.UserRepository).Returns(repository);
            var us = new UserService(uow.Object);
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
                using var context = new FileStorageContext(FakeDbContext.GetUnitTestDbOptions());
                UserRepository repository = new UserRepository(context);
                var uow = new Mock<IUnitOfWork>();
                uow.Setup(res => res.UserRepository).Returns(repository);
                var us = new UserService(uow.Object);
                await us.AddAsync(new User { Login = "NewLogin", Password = "NewPassword", Name = "NewName", Email = "NewEmail@gmail.com", LastName = "NewLastName" });
                context.SaveChanges();
                Assert.That(us.GetAll().Count, Is.EqualTo(3));
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
                using var context = new FileStorageContext(FakeDbContext.GetUnitTestDbOptions());
                UserRepository repository = new UserRepository(context);
                var uow = new Mock<IUnitOfWork>();
                uow.Setup(res => res.UserRepository).Returns(repository);
                var us = new UserService(uow.Object);
                await us.ChangeAll("Murk", "Markkkk", "Mark123123", "Amosov", "Mark", "Mark@gmail.com");
                context.SaveChanges();
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
                using var context = new FileStorageContext(FakeDbContext.GetUnitTestDbOptions());
                UserRepository repository = new UserRepository(context);
                var uow = new Mock<IUnitOfWork>();
                uow.Setup(res => res.UserRepository).Returns(repository);
                var us = new UserService(uow.Object);
                await us.ChangeAll("Murk", obj.Login, obj.Password, obj.Name, obj.LastName, obj.Email);
                context.SaveChanges();
                Assert.Fail();
            }
            catch (AuthException)
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
                using var context = new FileStorageContext(FakeDbContext.GetUnitTestDbOptions());
                UserRepository repository = new UserRepository(context);
                var uow = new Mock<IUnitOfWork>();
                uow.Setup(res => res.UserRepository).Returns(repository);
                var us = new UserService(uow.Object);
                await us.DeleteAsync(1);
                context.SaveChanges();
                Assert.That(us.GetAll().Count, Is.EqualTo(1));
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
                using var context = new FileStorageContext(FakeDbContext.GetUnitTestDbOptions());
                UserRepository repository = new UserRepository(context);
                FullFileInfoRepository repository1 = new FullFileInfoRepository(context);
                var uow = new Mock<IUnitOfWork>();
                uow.Setup(res => res.UserRepository).Returns(repository);
                uow.Setup(res => res.FullFileInfoRepository).Returns(repository1);
                var us = new UserService(uow.Object);
                await us.DeleteAsyncByLogin("Ksy");
                context.SaveChanges();
                Assert.That(us.GetAll().Count, Is.EqualTo(1));
            }
            catch
            {
                Assert.Fail();
            }

        }

    }
}
