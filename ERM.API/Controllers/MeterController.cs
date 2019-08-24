using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ERM.API.Entities;
using ERM.DataAccess;
using ERM.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ERM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeterController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<MeterController> logger;
        private readonly IMapper mapper;

        public MeterController(IUnitOfWork unitOfWork, ILogger<MeterController> logger, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.mapper = mapper;
        }

        // GET api/meter
        /// <summary>
        /// Exceptions are handled in Global Exception in the middleware in StartUp.cs
        /// Logging of necessare information are done in debug mode. Ideally in a sink like CosmosDB
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ERMSinkTableDto>>> GetSinkDataAsync()
        {
            logger.LogInformation("Getting all the data");
            var ermSinkData = await unitOfWork.ERMSinktable.GetAllERMSinkDataAsync();

            if (ermSinkData.Count() == 0)
            {
                logger.LogInformation("There is no data in the database");
                return NotFound();
            }

            logger.LogInformation("Data Found");
            return Ok(mapper.Map<List<ERMSinkTableDto>>(ermSinkData));
        }
    }
}
