﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.Service;
using IBM.WatsonDeveloperCloud.ToneAnalyzer.v3.Models;
using Newtonsoft.Json.Linq;
using System.Runtime.ExceptionServices;

namespace IBM.WatsonDeveloperCloud.ToneAnalyzer.v3
{
    public class ToneAnalizerService : WatsonService, IToneAnalyzerService
    {
        const string SERVICE_NAME = "tone_analyzer";

        const string PATH_TONE = "/v3/tone";
        const string VERSION_DATE_2016_05_19 = "2016-05-19";

        const string URL = "https://gateway.watsonplatform.net/tone-analyzer/api";

        public ToneAnalizerService()
            : base(SERVICE_NAME, URL)
        {
            if (!string.IsNullOrEmpty(this.Endpoint))
                this.Endpoint = URL;
        }

        public ToneAnalizerService(string userName, string password)
            : this()
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName));

            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));

            this.SetCredential(userName, password);
        }

        public ToneAnalizerService(IClient httpClient)
            : this()
        {
            if (httpClient == null)
                throw new ArgumentNullException(nameof(httpClient));

            this.Client = httpClient;
        }

        public ToneAnalysis AnalyzeTone(string text)
        {
            return this.AnalyzeTone(text, null, true);
        }

        public ToneAnalysis AnalyzeTone(string text, List<Tone> filterTones)
        {
            return this.AnalyzeTone(text, filterTones, true);
        }

        public ToneAnalysis AnalyzeTone(string text, List<Tone> filterTones, bool sentences = true)
        {
            ToneAnalysis result = null;

            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException("parameter: text");

            try
            {
                JObject json =
                    new JObject(
                            new JProperty("text", text));

                IRequest request =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                          .PostAsync(this.Endpoint + PATH_TONE)
                          .WithArgument("version", VERSION_DATE_2016_05_19)
                          .WithArgument("sentences", sentences);

                if (filterTones != null && filterTones.Count > 0)
                    request.WithArgument("tones", filterTones.Select(t => t.ToString().ToLower())
                                                             .Aggregate((a, b) => a + ", " + b));

                result =
                    request.WithBody<JObject>(json, MediaTypeHeaderValue.Parse(HttpMediaType.APPLICATION_JSON))
                           .As<ToneAnalysis>()
                           .Result;
            }
            catch (AggregateException ae)
            {
                ExceptionDispatchInfo.Capture(ae.Flatten().InnerException).Throw(); ;
            }

            return result;
        }
    }
}