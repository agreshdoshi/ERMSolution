using ERM.DataAccess.Models;
using ERM.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERM.DataAccess.Tests
{
    [TestClass]
    public class ERMSinkTableRepositoryTests
    {
        [TestMethod]
        public async Task GivenGetAll_WhenTestClassObjectIsPassed_ThenTheMethodIsCalled()
        {
            // Arrange 
            var testObject = new TestClass();
            var dbContext = new Mock<ERMSinkDbDbContext>();
            var dbSetMock = new Mock<DbSet<TestClass>>();
            var sinkTable = new ErmsinkTable();
            

            dbContext.Setup(x => x.Set<TestClass>()).Returns(dbSetMock.Object);


            // Act
            var repository = new Repository<TestClass>(dbContext.Object);
            var sut = new  ERMSinkTableRepository(dbContext.Object);
            var sut1 = await sut.GetAllERMSinkDataAsync();

            // Assert
            Assert.IsInstanceOfType(typeof(Task<IEnumerable<ErmsinkTable>>), sut.GetType());
        }

     }

  
}
