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
        [Test]
        public void UserRepository_GetAllAsync_ReturnsAllValues()
        {
            using var context = new FileStorageContext(FakeDbContext.GetUnitTestDbOptions());
            UserRepository repository = new UserRepository(context);
            var user = repository.GetAll();
            Assert.That(user.Count, Is.EqualTo(2));
        }
        [Test]
        public void UserRepository_DeleteById_ReturnsCorrectValues()
        {
            using var context = new FileStorageContext(FakeDbContext.GetUnitTestDbOptions());
            UserRepository repository = new UserRepository(context);
            repository.DeleteById(2);
            context.SaveChanges();
            var user = repository.GetAll();
            Assert.That(user.Count, Is.EqualTo(1));
        }
        [Test]
        public async Task UserRepository_GetById_ReturnsCorrectValues()
        {
            using var context = new FileStorageContext(FakeDbContext.GetUnitTestDbOptions());
            UserRepository repository = new UserRepository(context);
            var user = await repository.GetByIdAsync(1);
            Assert.That(user.Login, Is.EqualTo("Murk"));
            Assert.That(user.Password, Is.EqualTo("Mark123"));
            Assert.That(user.Name, Is.EqualTo("Mark"));
            Assert.That(user.LastName, Is.EqualTo("Amosov"));
            Assert.That(user.Email, Is.EqualTo("380669978812mark@gmail.com"));
            Assert.That(user.Id, Is.EqualTo(1));
        }
        [Test]
        public void UserRepository_AddNewUser_ReturnsCorrectValues()
        {
            using var context = new FileStorageContext(FakeDbContext.GetUnitTestDbOptions());
            UserRepository repository = new UserRepository(context);
            repository.Add(new User { Id = 3, Login = "Dimon", Password = "Dimon13", LastName = "Tsepyh", Name = "Dima", Email = "Dima@gmail.com" });
            context.SaveChanges();
            var user = repository.GetAll();
            Assert.That(user.Count, Is.EqualTo(3));
        }
        [Test]
        public async Task UserRepository_Update_ReturnsCorrectValues()
        {
            using var context = new FileStorageContext(FakeDbContext.GetUnitTestDbOptions());
            UserRepository repository = new UserRepository(context);
            var user = await repository.GetByIdAsync(1);
            user.Name = "Markkk";
            user.Password = "Markkk";
            repository.Update(user);
            var user2 = await repository.GetByIdAsync(1);
            Assert.That(user.Login, Is.EqualTo("Murk"));
            Assert.That(user.Password, Is.EqualTo("Markkk"));
            Assert.That(user.Name, Is.EqualTo("Markkk"));
            Assert.That(user.LastName, Is.EqualTo("Amosov"));
            Assert.That(user.Email, Is.EqualTo("380669978812mark@gmail.com"));
            Assert.That(user.Id, Is.EqualTo(1));
        }
    }
}