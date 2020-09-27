using System;
using System.Collections.Generic;
using System.Text;

namespace MyMemory.Api
{
    public class StatusRequest
    {
        public ApiKey ApiKey { get; }

        public string Description { get; }

        public ImportOutcome? Status { get; }

        public StatusRequest(
            ApiKey apiKey,
            string description = null,
            ImportOutcome? status = null)
        {
            ApiKey = apiKey;
            Description = description;
            Status = status;
        }
    }
}
