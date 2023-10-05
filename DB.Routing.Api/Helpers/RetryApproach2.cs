using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DB.Routing.Api.Helpers
{
    public static class RetryApproach2
    {
        // Enum representing the back-off strategy to use. Required parameter for DoActionWithRetry()
        public enum BackOffStrategy
        {
            Linear = 1,
            Exponential = 2
        }

        // Retry a specific codeblock wrapped in an Action delegate
       public static void DoActionWithRetry(Action action, int maxRetries, int waitBetweenRetrySec, BackOffStrategy retryStrategy)
        {
            if (action == null)
            {
                throw new ArgumentNullException("No action specified");
            }

            int retryCount = 1;
            while (retryCount <= maxRetries)
            {
                try
                {
                    action();
                    break;
                }
                catch (Exception ex)
                {
                    if (maxRetries <= 0)
                    {
                        throw;
                    }
                    else
                    {
                        //Maybe Log the number of retries
                        Console.WriteLine("Encountered exception {0}, retrying operation", ex.ToString());

                        TimeSpan sleepTime;
                        if (retryStrategy == BackOffStrategy.Linear)
                        {
                            //Wait time is Fixed
                            sleepTime = TimeSpan.FromSeconds(waitBetweenRetrySec);
                        }
                        else
                        {
                            //Wait time increases exponentially
                            sleepTime = TimeSpan.FromSeconds(Math.Pow(waitBetweenRetrySec, retryCount));
                        }

                        Thread.Sleep(sleepTime);

                        retryCount++;
                    }
                }
            }
        }


    }
}
