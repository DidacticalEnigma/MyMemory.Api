using System.Collections.Generic;

namespace MyMemory.Api
{
    public class TranslationResponse
    {
        public IEnumerable<TranslationMatch> Matches { get; }

        public bool QuotaFinished { get; }

        public TranslationResponse(IEnumerable<TranslationMatch> matches, bool quotaFinished)
        {
            Matches = matches;
            QuotaFinished = quotaFinished;
        }
    }
}