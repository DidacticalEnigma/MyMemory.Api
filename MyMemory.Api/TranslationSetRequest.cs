using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace MyMemory.Api
{
    public class TranslationSetRequest
    {
        public string SourceText { get; }

        public string TranslatedText { get; }

        public string SourceLanguage { get; }

        public string TargetLanguage { get; }

        public ApiKey ApiKey { get; }

        public MailAddress MailAddress { get; }

        public TranslationSetRequest(
            string sourceText,
            string translatedText,
            string sourceLanguage,
            string targetLanguage,
            ApiKey apiKey = default,
            MailAddress mailAddress = null)
        {
            SourceText = sourceText ?? throw new ArgumentNullException(nameof(sourceText));
            TranslatedText = translatedText ?? throw new ArgumentNullException(nameof(translatedText));
            SourceLanguage = sourceLanguage ?? throw new ArgumentNullException(nameof(sourceLanguage));
            TargetLanguage = targetLanguage ?? throw new ArgumentNullException(nameof(targetLanguage));
            ApiKey = apiKey;
            MailAddress = mailAddress;
        }
    }
}
