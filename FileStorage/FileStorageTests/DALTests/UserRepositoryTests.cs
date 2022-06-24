using DAL.Data;
using DAL.Entities;
using DAL.Repositories;
using FileStorageTests.Helpers;
using Moq;

namespace FileStorageTests.DALTests
{
    [TestFixture]
    internal class UserRepositoryTests
    {
        private FakeDbSet<User> userDbSet;
        private Mock<FileStorageContext> contextMock;
        private UserRepository repository;
        [SetUp]
        public void Reload()
        {
            userDbSet = new FakeUserDbSet();
            contextMock = new Mock<FileStorageContext>();
            contextMock.Setup(dbContext => dbContext.User).Returns(userDbSet);
            repository = new UserRepository(contextMock.Object);
            SeedData();
        }

        private void SeedData()
        {
            repository.Add(new User { Id = 1, Login = "MaRkOn4iK", Password = "Mark123123", LastName = "Amosov", Name = "Mark", Email = "Mark@gmail.com" });
            repository.Add(new User { Id = 2, Login = "Deniska", Password = "Deniska12345", LastName = "Lipov", Name = "Denis", Email = "Denis@gmail.com" });
            repository.Add(new User { Id = 3, Login = "Kirryha", Password = "Kirryha54321", LastName = "Zolov", Name = "Kirill", Email = "Kirill@gmail.com" });
            repository.Add(new User { Id = 4, Login = "Irishka", Password = "Irka15144", LastName = "Kolova", Name = "Irina", Email = "Irina@gmail.com" });
        }

        [Test]
        public void UserRepository_GetAllAsync_ReturnsAllValues()
        {

            var user = repository.GetAll();
            Assert.That(user.Count, Is.EqualTo(4));
        }
        [Test]
        public void UserRepository_DeleteById_ReturnsCorrectValues()
        {
            repository.DeleteById(2);
            var user = repository.GetAll();
            Assert.That(user.Count, Is.EqualTo(3));
        }
        [Test]
        public async Task UserRepository_GetById_ReturnsCorrectValues()
        {

            var user = await repository.GetByIdAsync(1);
            Assert.That(user.Login, Is.EqualTo("MaRkOn4iK"));
            Assert.That(user.Password, Is.EqualTo("Mark123123"));
            Assert.That(user.Name, Is.EqualTo("Mark"));
            Assert.That(user.LastName, Is.EqualTo("Amosov"));
            Assert.That(user.Email, Is.EqualTo("Mark@gmail.com"));
            Assert.That(user.Id, Is.EqualTo(1));
        }
        [Test]
        public void UserRepository_AddNewUser_ReturnsCorrectValues()
        {
            repository.Add(new User { Id = 5, Login = "Dimon", Password = "Dimon13", LastName = "Tsepyh", Name = "Dima", Email = "Dima@gmail.com" });
            var user = repository.GetAll();
            Assert.That(user.Count, Is.EqualTo(5));
        }
        [Test]
        public async Task UserRepository_Update_ReturnsCorrectValues()
        {

            var user = await repository.GetByIdAsync(1);
            user.Name = "Markkk";
            repository.Update(user);
            var user2 = await repository.GetByIdAsync(1);
            Assert.That(user2.Login, Is.EqualTo("MaRkOn4iK"));
            Assert.That(user2.Password, Is.EqualTo("Mark123123"));
            Assert.That(user2.Name, Is.EqualTo("Markkk"));
            Assert.That(user2.LastName, Is.EqualTo("Amosov"));
            Assert.That(user2.Email, Is.EqualTo("Mark@gmail.com"));
            Assert.That(user2.Id, Is.EqualTo(1));
        }
    }
}