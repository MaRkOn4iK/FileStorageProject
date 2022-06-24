using DAL.Data;
using DAL.Entities;
using Moq;
using System.Data.Entity;

namespace FileStorageTests.DALTests
{
    [TestFixture]
    internal class UserRepositoryTests
    {
        static Mock mockSet = new Mock<DbSet<User>>();
        [Test]
        public void a() {
            Moq.Language.Flow.IReturnsResult<FileStorageContext> mockContext = new Mock<FileStorageContext>().Setup(m => m.Set<User>()).Returns(() => mockSet);
        }
    }
}