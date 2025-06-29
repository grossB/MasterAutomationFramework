using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Polly;
using Polly.Retry;

namespace MasterAutomationFramework.Common.Helper
{
    public static class RetryActionHelper
    {
        public static IWebDriver Driver { get; set; }

        public static T WaitUntil<T>(Func<IWebDriver, T> condition, int seconds, int pollingIntervalMilisec = 250, Type[] ignoreExceptionTypes = null)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(seconds))
            {
                PollingInterval = TimeSpan.FromMilliseconds(pollingIntervalMilisec),
            };
            if (ignoreExceptionTypes != null)
            {
                wait.IgnoreExceptionTypes(ignoreExceptionTypes);
            }

            return wait.Until(condition);
        }

        public static void WaitUntil(Func<bool> condition, int seconds, int pollingIntervalMiliseconds = 250, Type[] ignoreExceptionTypes = null)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(seconds))
            {
                PollingInterval = TimeSpan.FromMilliseconds(pollingIntervalMiliseconds),
            };
            wait.IgnoreExceptionTypes(ignoreExceptionTypes ?? new Type[0]);
            wait.Until(driver => { return condition(); });
        }

        public static void WaitUntil(Action condition, int seconds, int pollingIntervalMilisec = 250, Type[] ignoreExceptionTypes = null)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(seconds))
            {
                PollingInterval = TimeSpan.FromMilliseconds(pollingIntervalMilisec),
            };
            wait.IgnoreExceptionTypes(ignoreExceptionTypes ?? new Type[0]);
            wait.Until(
                driver =>
                {
                    condition();
                    return true;
                });
        }

        public static T RetryWithResult<T>(Func<T> actionToRun, TimeSpan timeout, string additionalTimeoutMessage = null, Action actionToBeTriggerdOnTimeout = null)
        {
            var timeoutPolicy = Policy.Timeout(
                timeout,
                onTimeout: (context, timespan, task) =>
                {
                    // Add extra logic to be invoked when a timeout occurs, such as logging
                    if (!string.IsNullOrEmpty(additionalTimeoutMessage))
                    {
                        Console.WriteLine(additionalTimeoutMessage);
                    }

                    if (actionToBeTriggerdOnTimeout != null)
                    {
                        actionToBeTriggerdOnTimeout.Invoke();
                    }
                });

            var exception = Policy.Handle<Exception>().WaitAndRetryForever(sleep => TimeSpan.FromSeconds(1));

            var result = Policy.Wrap(timeoutPolicy, exception).Execute(() => { return actionToRun.Invoke(); });

            return result;
        }

        public static void RunActionWithRetries(Action action, int retryCount, TimeSpan sleepDuration = default)
        {
            RetryPolicy(retryCount, sleepDuration).ExecuteAndCapture(action).FinalException?.ThrowFinalException();
        }

        // Example
        //      Policy.Handle<AssertionException>().WaitAndRetry(20, time => TimeSpan.FromSeconds(3)).Execute(
        //      () =>
        //      {
        //          var value = new JavaScriptSerializer().Deserialize<HealthMonitorModel>(client.Execute(request).Content).Checks.Last().Value;
        //          RetryActionHelper.CheckResultTrue(value == expectedVersion, "Verify if CMS version is updated");
        //          return true;
        //      });
        public static void CheckResultTrue(bool compareResult, string msg)
        {
            if (!compareResult)
            {
                throw new Exception($"AssertionException {msg}");
            }
        }

        public static T RunActionWithRetries<T>(Func<T> action, int retryCount, TimeSpan sleepDuration = default)
        {
            var result = RetryPolicy(retryCount, sleepDuration).ExecuteAndCapture(action);
            result.FinalException?.ThrowFinalException();

            return result.Result;
        }

        private static RetryPolicy RetryPolicy(int retryCount, TimeSpan sleepDuration = default)
        {
            var policy = Policy.Handle<Exception>().WaitAndRetry(retryCount, _ => sleepDuration == default ? TimeSpan.FromSeconds(1) : sleepDuration);
            return policy;
        }

        private static void ThrowFinalException(this Exception result)
        {
            throw result;
        }

        // public static void Retry(Action actionToRun, TimeSpan timeout, string additionalTimeoutMessage = null, Action actionToBeTriggerdOnTimeout = null)
        // {
        //    var timeoutPolicy = Policy.Timeout(timeout, onTimeout: (context, timespan, task) =>
        //    {
        //        // Add extra logic to be invoked when a timeout occurs, such as logging
        //        if (!string.IsNullOrEmpty(additionalTimeoutMessage))
        //        {
        //            Console.WriteLine(additionalTimeoutMessage);
        //        }
        //        if (actionToBeTriggerdOnTimeout != null)
        //        {
        //            actionToBeTriggerdOnTimeout.Invoke();
        //        }
        //    });

        // // (HandleInner matches exceptions at both the top-level and inner exceptions)
        //    var exception = Policy.HandleInner<Exception>().OrInner<TargetInvocationException>().WaitAndRetry(3, retryAttempt => TimeSpan.FromSeconds(10));

        // Policy.Wrap(timeoutPolicy, exception).Execute(() =>
        //    {
        //        actionToRun.Invoke();
        //    });
        // }
    }
}
