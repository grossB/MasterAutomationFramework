using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterAutomationFramework.Common.Models
{
    public class BrowserEntryLog
    {
        public BrowserEntryLog(string content)
        {
            Content = content;
        }

        public string Content { get; set; }
    }
}
