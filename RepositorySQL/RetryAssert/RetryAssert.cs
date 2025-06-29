using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorySQL.RetryAssert
{
    public static class RetryAssert
    {
        public static void Assert(Action action, int retryCount = 10)
        {
            var policy = RetryPolicy(retryCount);

            var result = policy.ExecuteAndCapture(action);

            if (result.FinalException != null)
            {
                throw result.FinalException;
            }
        }

        private static RetryPolicy RetryPolicy(int retryCount)
        {
            var policy = Policy
                .Handle<Exception>()
                .WaitAndRetry(retryCount, _ => TimeSpan.FromSeconds(1));
            return policy;
        }

        public static T Assert<T>(Func<T> action, int retryCount = 20)
        {
            var policy = RetryPolicy(retryCount);
            var result = policy.ExecuteAndCapture(action);

            if (result.FinalException != null)
            {
                throw result.FinalException;
            }

            return result.Result;
        }
    }
}
