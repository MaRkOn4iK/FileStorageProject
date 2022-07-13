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
    internal class AutorizationServiceTests
    {
        static object[] UsersWithNullField =
        {
        new User { Login = "", Password = "NicePassword", Name = "qwe", Email = "mark@gmail.com", LastName = "qwe" },
        new User { Login = "MaRkOn4iKMaRkOn4iK", Password = "bad", Name = "qwe", Email = "mark@gmail.com", LastName = "qwe" },
        new User { Login = "MaRkOn4iKMaRkOn4iK", Password = "NicePassword", Name = "", Email = "mark@gmail.com", LastName = "qwe" },
        new User { Login = "MaRkOn4iKMaRkOn4iK", Password = "NicePassword", Name = "qwe", Email = "markemailgmail.com", LastName = "qwe" },
        new User { Login = "MaRkOn4iKMaRkOn4iK", Password = "NicePassword", Name = "qwe", Email = "@mail.com", LastName = "qwe" }

        };

        [Test]
        public void AutorizationService_Authenticate_ReturnCorrectValue()
        {
            using var context = new FileStorageContext(FakeDbContext.GetUnitTestDbOptions());
            UserRepository repository = new UserRepository(context);
            var uow = new Mock<IUnitOfWork>();
            uow.Setup(res => res.UserRepository).Returns(repository);
            var aserv = new AutorizationService(uow.Object);
            var tmp = uow.Object.UserRepository.GetByIdAsync(1);
            var user = aserv.Authenticate(tmp.Result.Login, tmp.Result.Password);
            Assert.That(user.Login, Is.EqualTo("Murk"));
            Assert.That(user.LastName, Is.EqualTo("Amosov"));
        }
        [Test]
        public void AutorizationService_Authenticate_ThrowFileStorageException()
        {
            try
            {
                using var context = new FileStorageContext(FakeDbContext.GetUnitTestDbOptions());
                UserRepository repository = new UserRepository(context);
                var uow = new Mock<IUnitOfWork>();
                uow.Setup(res => res.UserRepository).Returns(repository);
                var aserv = new AutorizationService(uow.Object);
                var user = aserv.Authenticate("Who", "Who");
                Assert.Fail();

            }
            catch (AuthException ex)
            {
                Assert.That(ex.Message, Is.EqualTo("Incorrect login or password"));
            }
            catch
            {
                Assert.Fail();
            }
        }

        [Test]
        public void AutorizationService_GetUserByLogin_ReturnCorrectValue()
        {
            using var context = new FileStorageContext(FakeDbContext.GetUnitTestDbOptions());
            UserRepository repository = new UserRepository(context);
            var uow = new Mock<IUnitOfWork>();
            uow.Setup(res => res.UserRepository).Returns(repository);
            var aserv = new AutorizationService(uow.Object);
            var tmp = uow.Object.UserRepository.GetByIdAsync(1);
            var user = aserv.GetUserByLogin(tmp.Result.Login);
            Assert.That(user.Login, Is.EqualTo("Murk"));
            Assert.That(user.LastName, Is.EqualTo("Amosov"));
        }
        [Test]
        public void AutorizationService_GetUserByLogin_ThrowFileStorageException()
        {
            try
            {
                using var context = new FileStorageContext(FakeDbContext.GetUnitTestDbOptions());
                UserRepository repository = new UserRepository(context);
                var uow = new Mock<IUnitOfWork>();
                uow.Setup(res => res.UserRepository).Returns(repository);
                var aserv = new AutorizationService(uow.Object);
                var user = aserv.GetUserByLogin("Who");
                Assert.Fail();

            }
            catch (AuthException ex)
            {
                Assert.That(ex.Message, Is.EqualTo("Incorrect login"));
            }
            catch
            {
                Assert.Fail();
            }
        }

        [Test]
        public void AutorizationService_Registration_ReturnCorrectValue()
        {
            using var context = new FileStorageContext(FakeDbContext.GetUnitTestDbOptions());
            UserRepository repository = new UserRepository(context);
            var uow = new Mock<IUnitOfWork>();
            uow.Setup(res => res.UserRepository).Returns(repository);
            var aserv = new AutorizationService(uow.Object);
            var user = aserv.Registration("NewUser", "NewUserPassword", "NewUserName", "NewUserLastName", "NewUserEmail@gmail.com");
            context.SaveChanges();
            Assert.That(user.Login, Is.EqualTo("NewUser"));
            Assert.That(user.LastName, Is.EqualTo("NewUserLastName"));
            Assert.That(uow.Object.UserRepository.GetAll().Count, Is.EqualTo(3));
        }
        [TestCaseSource(nameof(UsersWithNullField))]
        public async Task AutorizationService_Registration_ThrowFileStorageException(User obj)
        {
            try
            {
                using var context = new FileStorageContext(FakeDbContext.GetUnitTestDbOptions());
                UserRepository repository = new UserRepository(context);
                var uow = new Mock<IUnitOfWork>();
                uow.Setup(res => res.UserRepository).Returns(repository);
                var aserv = new AutorizationService(uow.Object);
                var user = aserv.Registration(obj.Login, obj.Password, obj.Name, obj.LastName, obj.Email);
                context.SaveChanges();
                Assert.Fail();
            }
            catch
            {
               
            }

        }
    }
}
