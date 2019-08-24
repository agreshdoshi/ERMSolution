using ERM.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace ERM.DataAccess.Tests
{
    [TestClass]
    public class RepositoryTests
    {
        [TestMethod]
        public void GivenGetAll_WhenTestClassObjectIsPassed_ThenTheMethodIsCalled()
        {
            // Arrange 
            var testObject = new TestClass();
            var dbContext = new Mock<DbContext>();
            var dbSetMock = new Mock<DbSet<TestClass>>();
            dbContext.Setup(x => x.Set<TestClass>()).Returns(dbSetMock.Object);


            // Act
            var sut = new Repository<TestClass>(dbContext.Object).GetAll();


            // Assert
            dbContext.Verify(x => x.Set<TestClass>());
        }

     }

}
