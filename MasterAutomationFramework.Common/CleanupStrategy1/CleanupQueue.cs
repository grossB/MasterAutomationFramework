using MasterAutomationFramework.Common.Extension;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.ExceptionServices;

namespace MasterAutomationFramework.Common.CleanupStrategy1
{
    [ExcludeFromCodeCoverage]
    public class CleanupQueue : ICleanupQueue
    {
        private Stack<Action> _cleanupActions = new Stack<Action>();

        public void AddCleanupStep(Action step)
        {
            _cleanupActions.Push(step);
        }

        public void Cleanup(Ilogger logger = null)
        {
            var exceptions = new List<ExceptionDispatchInfo>();
            while (!_cleanupActions.IsEmpty())
            {
                var action = _cleanupActions.Pop();
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    exceptions.Add(ExceptionDispatchInfo.Capture(ex));
                    logger?.WriteLine("Exception occurred in cleanup:");
                    logger?.WriteLine(ex.ToString());
                }
            }
        }
    }
}
