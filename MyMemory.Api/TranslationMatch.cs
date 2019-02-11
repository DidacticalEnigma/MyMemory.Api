using System;

namespace MyMemory.Api
{
    public class TranslationMatch
    {
        public long Id { get; }

        public string Segment { get; }

        public string Translation { get; }

        public int Quality { get; }

        // reference

        public string Subject { get; }

        public string CreatedBy { get; }

        public string LastUpdatedBy { get; }

        public DateTime CreationTime { get; }

        public DateTime LastUpdateTime { get; }

        public int Match { get; }

        public TranslationMatch(
            long id,
            string segment,
            string translation,
            int quality,
            string subject,
            string createdBy,
            string lastUpdatedBy,
            DateTime createDate,
            DateTime lastUpdateDate,
            int match)
        {
            Id = id;
            Segment = segment;
            Translation = translation;
            Quality = quality;
            Subject = subject;
            CreatedBy = createdBy;
            LastUpdatedBy = lastUpdatedBy;
            CreationTime = createDate;
            LastUpdateTime = lastUpdateDate;
            Match = match;
        }
    }
}