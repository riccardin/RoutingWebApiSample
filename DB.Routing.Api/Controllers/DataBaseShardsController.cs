using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using DB.Routing.Api.Helpers;
using DB.Routing.Api.Models;
using Microsoft.Extensions.Logging;
using DB.Routing.Api.Contracts;


namespace DB.Routing.Api.Controllers
{
    [Route("api/DataBaseShards")]
    public class DataBaseShardsController : Controller
    {

        private ILogger _logger;
        private IShardInfoRepository _shardRepository;
        public DataBaseShardsController(ILogger<DataBaseShardsController> logger, IShardInfoRepository shardRepository ) {

            _logger = logger;
            _shardRepository = shardRepository;
        }



     [HttpGet]
        public IActionResult GetAllShards()
        {
            List<Models.ShardInformation> shardInfoList = null;
            shardInfoList = new List<Models.ShardInformation>();
            for (int i = 0; i < 4; i++)
            {

                var shardInfo = new Models.ShardInformation
                {
                    id = Guid.NewGuid(),
                    connectionString = $"Initial Catalog=ArchiveDB_{i};Network Address=zdns_ARCHIVE01,21433;Trusted_Connection=Yes;",
                    customerId = i
                };


                shardInfoList.Add(shardInfo);
            }

            if (shardInfoList == null) {
                return NotFound();
            }

            //return StatusCode(500, "An unexpected fault happened. Try again later.");

            var dbShardsInfo = Mapper.Map<IEnumerable<Models.ShardInformationDto>>(shardInfoList);
            return Ok(dbShardsInfo);
        }

        
        [HttpGet("{id}", Name ="GetShardInfo")]
        [HttpHead]
        public IActionResult GetShardInfobyGroupId(int id)
        {

            //throw new Exception("Exeception which proves Global Error handling works");

            var shardInfo = new Models.ShardInformation
            {
                id = Guid.NewGuid(),
                connectionString = $"Initial Catalog=ArchiveDB_{id};Network Address=zdns_ARCHIVE01,21433;Trusted_Connection=Yes;",
                customerId = id
            };


            var dbShardsInfo = Mapper.Map<Models.ShardInformationDto>(shardInfo);

            return Ok(dbShardsInfo);
        }

     

      
        [HttpPost]
        public IActionResult CreateDBShard([FromBody]ShardInformationDto shardInfo)
        {
          

            if (shardInfo == null)
            {
                return BadRequest();
            }

           
            //Custom Error
            if (shardInfo.connectionString == "test") {

                ModelState.AddModelError(nameof(ShardInformationDto), 
                    "The provided connection string should be different to test");
                _logger.LogInformation(100,"The provided connection string should be different to test");
            }

            if (!ModelState.IsValid)
            {
                //return 422 StatusCode
                return new UnprocesseableEntityObjectResult(ModelState);

            }

            //if an error happens let's leave it to the middleware to handle it
            //throw new Exception("Create a new Shard failed on save.");

            var shardInfoToReturn = _shardRepository.getShardInfo();
          




            return CreatedAtRoute("GetShardInfo",  //Adds the location in the Header of the response. 
                new { id = shardInfoToReturn.id }, 
                shardInfoToReturn);
        }


        [HttpPut]
        public IActionResult UpdateDBShard([FromBody]ShardInformationDto shardInfo)
        {

            var shardInfoToReturn = _shardRepository.getShardInfo();


            return Ok(shardInfoToReturn);


        }

        [HttpOptions]
        public IActionResult GetShardsOptions() {

            Response.Headers.Add("Allow", "GET,OPTIONS,POST,PUT");
            return Ok();

        }

            //[HttpPost]
            //public IActionResult CreateDBShardforCustomer(int customerId, [FromBody]dynamic data)
            //{

            //    if (data.value == null)
            //    {
            //        return BadRequest();
            //    }

            //    //Check if CustomerId existe
            //    //return NotFound();


            //    //return CreatedAtRoute("GetShardInfo",  //Adds the location in the Header of the response. 
            //    //    new { id = shardInfoToReturn.id },
            //    //    shardInfoToReturn);

            //    return Ok();

            //}



            //// PUT api/values/5
            //[HttpPut("{id}")]
            //public void Put(int id, [FromBody]string value)
            //{
            //}

            //// DELETE api/values/5
            //[HttpDelete("{id}")]
            //public void Delete(int id)
            //{
            //}
        }
}
