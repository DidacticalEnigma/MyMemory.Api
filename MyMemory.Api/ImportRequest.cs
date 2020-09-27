using System;
using System.IO;

namespace MyMemory.Api
{
    public class ImportRequest
    {
        public Stream Input { get; }

        public string FileName { get; }

        public ApiKey ApiKey { get; }

        /// <summary>
        /// Short import description
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// The subject of the TM you are importing.
        /// Use subjects API for a complete list
        /// </summary>
        public string Subject { get; }

        /// <summary>
        /// Whether you want to make it available to other people or just to you
        /// </summary>
        public bool? IsPrivate { get; }

        /// <summary>
        /// Context information for source language, in the form of a URL
        /// </summary>
        public Uri SourceUrl { get; }

        /// <summary>
        /// Context information for target language, in the form of a URL
        /// </summary>
        public Uri TargetUrl { get; }

        public ImportRequest(
            Stream input,
            string fileName,
            ApiKey apiKey = default,
            string description = null,
            string subject = null,
            bool? isPrivate = null,
            Uri sourceUrl = null,
            Uri targetUrl = null)
        {
            Input = input;
            FileName = fileName;
            ApiKey = apiKey;
            Description = description;
            Subject = subject;
            IsPrivate = isPrivate;
            SourceUrl = sourceUrl;
            TargetUrl = targetUrl;
        }

        public ImportRequest(
            ImportRequest cloneRequest,
            Stream input,
            string fileName,
            ApiKey? apiKey = null,
            string description = null,
            string subject = null,
            bool? isPrivate = null,
            Uri sourceUrl = null,
            Uri targetUrl = null)
        {
            Input = input;
            FileName = fileName;
            ApiKey = apiKey ?? cloneRequest.ApiKey;
            Description = description ?? cloneRequest.Description;
            Subject = subject ?? cloneRequest.Subject;
            IsPrivate = isPrivate ?? cloneRequest.IsPrivate;
            SourceUrl = sourceUrl ?? cloneRequest.SourceUrl;
            TargetUrl = targetUrl ?? cloneRequest.TargetUrl;
        }
    }
}
