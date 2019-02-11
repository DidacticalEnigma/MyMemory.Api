using System;
using System.Net;
using System.Net.Mail;

namespace MyMemory.Api
{
    public class TranslationRequest
    {
        public string QueriedText { get; }

        public string SourceLanguage { get; }

        public string TargetLanguage { get; }

        public bool MachineTranslated { get; }

        public bool PrivateMatchesOnly { get; }

        public ApiKey ApiKey { get; }

        public IPAddress SourceIp { get; }

        public MailAddress MailAddress { get; }

        public TranslationRequest(string queriedText,
            string sourceLanguage,
            string targetLanguage,
            bool machineTranslated = true,
            bool privateMatchesOnly = false,
            ApiKey apiKey = default,
            IPAddress sourceIp = null,
            MailAddress mailAddress = null)
        {
            QueriedText = queriedText ?? throw new ArgumentNullException(nameof(queriedText));
            SourceLanguage = sourceLanguage ?? throw new ArgumentNullException(nameof(sourceLanguage));
            TargetLanguage = targetLanguage ?? throw new ArgumentNullException(nameof(targetLanguage));
            MachineTranslated = machineTranslated;
            PrivateMatchesOnly = privateMatchesOnly;
            ApiKey = apiKey;
            SourceIp = sourceIp;
            MailAddress = mailAddress;
        }

        public TranslationRequest(TranslationRequest cloneRequest,
            string queriedText,
            string sourceLanguage = null,
            string targetLanguage = null,
            bool? machineTranslated = null,
            bool? privateMatchesOnly = null,
            ApiKey? apiKey = null,
            IPAddress sourceIp = null,
            MailAddress mailAddress = null)
        {
            QueriedText = queriedText ?? throw new ArgumentNullException(nameof(queriedText));
            SourceLanguage = sourceLanguage ?? cloneRequest.SourceLanguage;
            TargetLanguage = targetLanguage ?? cloneRequest.TargetLanguage;
            MachineTranslated = machineTranslated ?? cloneRequest.MachineTranslated;
            PrivateMatchesOnly = privateMatchesOnly ?? cloneRequest.PrivateMatchesOnly;
            ApiKey = apiKey ?? cloneRequest.ApiKey;
            this.SourceIp = sourceIp ?? cloneRequest.SourceIp;
            MailAddress = mailAddress ?? cloneRequest.MailAddress;
        }
    }
}