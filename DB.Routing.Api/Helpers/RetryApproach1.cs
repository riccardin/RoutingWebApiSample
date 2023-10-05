using System;
//using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;

namespace DB.Routing.Api.Helpers
{
   

    public static class RetryApproach1
    {
        internal static async Task<TResult> RetryLogic<TResult>(Func<Task<TResult>> work, TimeSpan retryTimeout = default(TimeSpan))
        {
            if (retryTimeout == default(TimeSpan))
                retryTimeout = TimeSpan.FromSeconds(60d);
            var millisecondsTimeout = 30;
            var startTime = DateTime.Now;
            var retryCount = 0;
            while (true)
            {
                retryCount++;
                try
                {
                    var result = await work();
                    return result;
                }
                //catch (FaultException)
                //{
                //    throw;
                //}
                catch (Exception )
                {
                    var timeSpan = DateTime.Now - startTime;
                    var random = new Random();
                    if (retryTimeout < timeSpan)
                    {
                        throw;
                    }
                    Thread.Sleep(millisecondsTimeout);
                    millisecondsTimeout = (1000 * retryCount) + random.Next(20, 100);
                }
            }
        }
    }



}
