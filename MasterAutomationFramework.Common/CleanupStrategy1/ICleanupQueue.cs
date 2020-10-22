using System;

namespace MasterAutomationFramework.Common.CleanupStrategy1
{
    public interface ICleanupQueue
    {
        void AddCleanupStep(Action step);
    }
}