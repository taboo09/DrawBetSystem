using System;

namespace BetSystem.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool GetShowRequestId() => !string.IsNullOrEmpty(RequestId);
    }
}