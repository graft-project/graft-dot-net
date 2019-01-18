using System;
using System.Collections.Generic;
using System.Text;

namespace GraftLib.Models
{
    public class GraftServiceConfiguration
    {
        public string ServerUrl { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        public bool RestorePreviousSession { get; set; } = false;
        public int TransactionCount { get; set; } = 20;
        public TimeSpan TransactionWaitTime { get; set; } = new TimeSpan(0, 2, 0);
        public TimeSpan TransactionStatusWaitTime { get; set; } = new TimeSpan(0, 2, 0);
    }
}
