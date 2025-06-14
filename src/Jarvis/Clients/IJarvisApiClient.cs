﻿using Jarvis.Utils;
using System.Text.Json;

namespace Jarvis.Clients
{
    public interface IJarvisApiClient
    {
        ITagsClient Tags { get; }

        ITaskClient Tasks { get; }

        IUserClient Users { get; }

        IWinTheDayClient WinTheDay { get; }

        IIntegrationClient Verifications { get; }
    }

    public class JarvisApiClient : IJarvisApiClient
    {
        private readonly HttpClient _client;

        public JarvisApiClient(HttpClient client)
        {
            _client = client;
        }

        public ITagsClient Tags => new TagsClient(_client);

        public ITaskClient Tasks => new TaskClient(_client);

        public IUserClient Users => new UserClient(_client);

        public IWinTheDayClient WinTheDay => new WinTheDayClient(_client);

        public IIntegrationClient Verifications => new IntegrationClient(_client);
    }
}
