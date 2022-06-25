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
    internal class AutorizationServiceTests
    {
        private FakeDbSet<User> userDbSet;
        private Mock<FileStorageContext> contextMock;
        private UserRepository repository;
        Mock<IUnitOfWork> uow;
        AutorizationService aserv;
        static object[] UsersWithNullField =
        {
       new User { Id = 1, Login = "MaRkOn4iK", Password = "Mark123123", LastName = "Amosov", Name = "Mark", Email = "Mark@gmail.com" },
        new User { Id = 2, Login = "Deniska", Password = "Deniska12345", LastName = "Lipov", Name = "Denis", Email = "Denis@gmail.com" },
        new User { Login = "", Password = "NicePassword", Name = "qwe", Email = "mark@gmail.com", LastName = "qwe" },
        new User { Login = "MaRkOn4iKMaRkOn4iK", Password = "bad", Name = "qwe", Email = "mark@gmail.com", LastName = "qwe" },
        new User { Login = "MaRkOn4iKMaRkOn4iK", Password = "NicePassword", Name = "", Email = "mark@gmail.com", LastName = "qwe" },
        new User { Login = "MaRkOn4iKMaRkOn4iK", Password = "NicePassword", Name = "qwe", Email = "markemailgmail.com", LastName = "qwe" },
        new User { Login = "MaRkOn4iKMaRkOn4iK", Password = "NicePassword", Name = "qwe", Email = "@mail.com", LastName = "qwe" }

        };
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
            aserv = new AutorizationService(uow.Object);
        }
        private void SeedData()
        {
            repository.Add(new User { Id = 1, Login = "MaRkOn4iK", Password = "Mark123123", LastName = "Amosov", Name = "Mark", Email = "Mark@gmail.com" });
            repository.Add(new User { Id = 2, Login = "Deniska", Password = "Deniska12345", LastName = "Lipov", Name = "Denis", Email = "Denis@gmail.com" });
            repository.Add(new User { Id = 3, Login = "Kirryha", Password = "Kirryha54321", LastName = "Zolov", Name = "Kirill", Email = "Kirill@gmail.com" });
            repository.Add(new User { Id = 4, Login = "Irishka", Password = "Irka15144", LastName = "Kolova", Name = "Irina", Email = "Irina@gmail.com" });
        }
        [Test]
        public void AutorizationService_Authenticate_ReturnCorrectValue()
        {
            var tmp = uow.Object.UserRepository.GetByIdAsync(1);
            var user = aserv.Authenticate(tmp.Result.Login, tmp.Result.Password);
            Assert.That(user.Login, Is.EqualTo("MaRkOn4iK"));
            Assert.That(user.LastName, Is.EqualTo("Amosov"));
        }
        [Test]
        public void AutorizationService_Authenticate_ThrowFileStorageException()
        {
            try
            {
            var user = aserv.Authenticate("Who", "Who");
            Assert.Fail();

            }
            catch (FileStorageException ex)
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
            var tmp = uow.Object.UserRepository.GetByIdAsync(1);
            var user = aserv.GetUserByLogin(tmp.Result.Login);
            Assert.That(user.Login, Is.EqualTo("MaRkOn4iK"));
            Assert.That(user.LastName, Is.EqualTo("Amosov"));
        }
        [Test]
        public void AutorizationService_GetUserByLogin_ThrowFileStorageException()
        {
            try
            {
                var user = aserv.GetUserByLogin("Who");
                Assert.Fail();

            }
            catch (FileStorageException ex)
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
            var user = aserv.Registration("NewUser","NewUserPassword","NewUserName","NewUserLastName","NewUserEmail@gmail.com");
            Assert.That(user.Login, Is.EqualTo("NewUser"));
            Assert.That(user.LastName, Is.EqualTo("NewUserLastName"));
            Assert.That(uow.Object.UserRepository.GetAll().Count,Is.EqualTo(5));
        }
        [TestCaseSource(nameof(UsersWithNullField))]
        public async Task AutorizationService_Registration_ThrowFileStorageException(User obj)
        {
            try
            {
                var user = aserv.Registration(obj.Login,obj.Password,obj.Name,obj.LastName,obj.Email);
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
    }
}
