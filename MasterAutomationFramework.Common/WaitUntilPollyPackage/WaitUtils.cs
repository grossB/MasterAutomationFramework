using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MasterAutomationFramework.SeleniumAPI.WaitUtils
{

    // Usage of basic Polly package functionality
    // https://github.com/App-vNext/Polly#retry
    public class WaitUtils
    {
        // Execute an action
        // var policy = Policy.Handle<KeyNotFoundException>().OrInner<NullReferenceException>().Retry();
        // policy.Execute(() => DoSomething());
        // Policy.Handle<KeyNotFoundException>().Retry(3);

        public void Execute(Action actionToExecute, TimeSpan timeout)
        {
            var timeoutPolicy = Policy.Timeout(timeout);
            timeoutPolicy.Execute(actionToExecute);
        }

        public void ExecuteIgnoreSpecificException<T>(Action actionToExecute, TimeSpan timeout) where T : Exception
        {
            var policy = Policy.Handle<T>().Retry();
            policy.Execute(actionToExecute);
        }

        public bool ExecuteIgnoreAnyException(Action actionToExecute, TimeSpan timeout)
        {
            try
            {
                this.Execute(actionToExecute, timeout);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private static void ValidateNullArgument(object arg, string argName)
        {
            if (arg == null)
                throw new ArgumentNullException(argName);
        }

        private static void ValidateTimeout(TimeSpan timeSpan, string paramName = "timeout")
        {
            if (timeSpan < TimeSpan.Zero)
                throw new ArgumentOutOfRangeException(paramName);
        }
    }
}
