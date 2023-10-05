using DB.Routing.Api.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DB.Routing.Api.Models;
using DB.Routing.Api.Helpers;

namespace DB.Routing.Api
{
    public class ShardInfoRepository:IShardInfoRepository
    {

        public ShardInformation getShardInfo() {

            ShardInformation shardInfo = null;
            try
            {
                RetryApproach2.DoActionWithRetry(() =>
                {

                    //Action to retry
                    getInfo();



                }, 3, 5, RetryApproach2.BackOffStrategy.Exponential);
            }
            catch (Exception ex)
            {
                //At this point you can either log the error or log the error and rethrow the exception, depending on your requirements
                Console.WriteLine("Exhausted all retries - exiting program",  ex.Source);
                throw;
            }

            
            return shardInfo;
        }

        public async Task<ShardInformation> getShardsInfoAsync()
        {
           return await RetryApproach1.RetryLogic(async () => await getInfoAsync());
        }


        private ShardInformation getInfo() {

            return  new ShardInformation
            {
                id = Guid.NewGuid(),
                connectionString = $"Initial Catalog=ArchiveDB_{1};Network Address=zdns_ARCHIVE01,21433;Trusted_Connection=Yes;",
                customerId = 1
            };

            
        }


        private async Task<ShardInformation> getInfoAsync()
        {
           return await Task.Run(() => getInfo());
           

        }



    }
}
