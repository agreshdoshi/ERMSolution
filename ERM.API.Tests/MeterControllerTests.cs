using AutoMapper;
using ERM.API.Controllers;
using ERM.DataAccess;
using ERM.DataAccess.Models;
using ERM.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERM.API.Tests
{
    [TestClass]
    public class MeterControllerTests
    {
        private Mock<IUnitOfWork> unitOfWork;
        private Mock<ILogger<MeterController>> logger;
        private Mock<IMapper> mapper;
        private Mock<IErmSinkTableRepository> repository;

        [TestInitialize]
        public void Startup()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            logger = new Mock<ILogger<MeterController>>();
            mapper = new Mock<IMapper>();
            repository = new Mock<IErmSinkTableRepository>();
        }

        [TestMethod]
        public async Task GivenGetSinkDataAsync_WhenThereIsData_ThenOkIsReturned()
        {
            // Arrange
            var sinkList = new List<ErmsinkTable>();
            sinkList.Add(new ErmsinkTable { DataType = "123" });
            repository.Setup(a => a.GetAllERMSinkDataAsync())
                .Returns(Task.FromResult<IEnumerable<ErmsinkTable>>(sinkList));
            unitOfWork.Setup(a => a.ERMSinktable).Returns(repository.Object);

            // Act
            var sut = new MeterController(unitOfWork.Object, logger.Object, mapper.Object);
            var result = await sut.GetSinkDataAsync();

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GivenGetSinkDataAsync_WhenThereIsNoData_ThenNotFoundIsReturned()
        {
            // Arrange
            var sinkList = new List<ErmsinkTable>();
            repository.Setup(a => a.GetAllERMSinkDataAsync())
                .Returns(Task.FromResult<IEnumerable<ErmsinkTable>>(sinkList));
            unitOfWork.Setup(a => a.ERMSinktable).Returns(repository.Object);

            // Act 
            var sut = new MeterController(unitOfWork.Object, logger.Object, mapper.Object);
            var result = await sut.GetSinkDataAsync();

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GivenGetSinkDataAsync_WhenThereTheBackendObjectIsNull_ThenErrorIsThrown()
        {
            // Arrange


            // Act
            var sut = new MeterController(unitOfWork.Object, logger.Object, mapper.Object);

            // Assert
            await Assert.ThrowsExceptionAsync<NullReferenceException>(() => sut.GetSinkDataAsync());
        }
    }
}