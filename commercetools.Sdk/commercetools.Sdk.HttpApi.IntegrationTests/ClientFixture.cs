﻿using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using commercetools.Sdk.DependencyInjection;
using commercetools.Sdk.HttpApi.Tokens;
using Polly;
using Polly.Extensions.Http;

namespace commercetools.Sdk.HttpApi.IntegrationTests
{
    public class ClientFixture
    {
        private readonly ServiceProvider serviceProvider;
        private readonly IConfiguration configuration;

        public ClientFixture()
        {
            var services = new ServiceCollection();

            //services.AddLogging(configure => configure.AddConsole());
            this.configuration = new ConfigurationBuilder().
                AddJsonFile("appsettings.test.json").
                AddJsonFile("appsettings.test.Development.json", true).
                // https://www.jerriepelser.com/blog/aspnet-core-no-more-worries-about-checking-in-secrets/
                AddEnvironmentVariables().
                AddUserSecrets<ClientFixture>().
                Build();

            services.UseCommercetools(configuration, "Client", TokenFlow.ClientCredentials);
            this.serviceProvider = services.BuildServiceProvider();
        }

        public T GetService<T>()
        {
            return this.serviceProvider.GetService<T>();
        }

        public IClientConfiguration GetClientConfiguration(string name)
        {
            return this.configuration.GetSection(name).Get<ClientConfiguration>();
        }
    }
}
